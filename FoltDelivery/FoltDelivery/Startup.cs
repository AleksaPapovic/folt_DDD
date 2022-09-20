using EventStore.ClientAPI;
using FoltDelivery.API.Repository;
using FoltDelivery.API.Service;
using FoltDelivery.Core.Authorization;
using FoltDelivery.Core.Commands;
using FoltDelivery.Core.Events;
using FoltDelivery.Core.EventStore;
using FoltDelivery.Core.Persistance;
using FoltDelivery.Core.Queries;
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
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
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

            services.AddSingleton(eventStoreConnection);
            EventBusExtensions.AddEventBus(services);

            services.AddMediatR(typeof(Startup))
                    .AddScoped<ICommandBus, CommandBus>()
                    .AddScoped<IQueryBus, QueryBus>();

            services.AddScoped<IMediator, Mediator>()
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEventStore, Core.EventStore.EventStore>();
            //services = Infrastructure.Queries.Config.AddAllQueryHandlers(services);

        }

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
