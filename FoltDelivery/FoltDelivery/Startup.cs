using EventStore.ClientAPI;
using FoltDelivery.API.Repository;
using FoltDelivery.API.Service;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Authorization;
using FoltDelivery.Infrastructure.Commands;
using FoltDelivery.Infrastructure.DataAccess;
using FoltDelivery.Infrastructure.Events;
using FoltDelivery.Infrastructure.Persistance;
using FoltDelivery.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System;

namespace FoltDelivery
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddControllers();

            var eventStoreConnection = EventStoreConnection.Create(
                connectionString: Configuration.GetValue<string>("EventStore:ConnectionString"),
                builder: ConnectionSettings.Create().KeepReconnecting(),
                connectionName: Configuration.GetValue<string>("EventStore:ConnectionName"));

            //services.AddSingleton(
            //   new EventStoreClient(EventStoreClientSettings.Create("esdb://localhost:2113?tls=false")));
            //eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();

            services.AddSingleton(eventStoreConnection);

            services.AddScoped<IDataAccess, DataAccess>();

            services.AddSingleton(sp => new EventBus(
           sp
           ));
            services
                .TryAddSingleton<IEventBus>(sp => sp.GetRequiredService<EventBus>());
            services.AddMediatR(typeof(DataAccess).Assembly)
                .AddScoped<ICommandBus, CommandBus>()
                .AddScoped<IQueryBus, QueryBus>();
            //.AddEventBus();
            services
                .AddScoped<IMediator, Mediator>()
                .AddTransient<ServiceFactory>(sp => sp.GetRequiredService!);

            services.AddAuthorization();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<FoltDeliveryDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("FoltAppCon"),
                    assembly => assembly.MigrationsAssembly(typeof(FoltDeliveryDbContext).Assembly.FullName)).UseLazyLoadingProxies();
            });

            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddTransient<IEventHandler<OrderPlaced>, OrderHandler>();
            //services.AddTransient<IEventHandler<OrderCreated>, OrderHandler>();
            //services = Infrastructure.Queries.Config.AddAllQueryHandlers(services);
            services.AddScoped<IEventStore, Infrastructure.EventStore>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
