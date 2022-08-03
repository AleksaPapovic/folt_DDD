using EventStore.ClientAPI;
using FoltDelivery.API.Repository;
using FoltDelivery.API.Service;
using FoltDelivery.Infrastructure;
using FoltDelivery.Infrastructure.Authorization;
using FoltDelivery.Infrastructure.Commands;
using FoltDelivery.Infrastructure.Events;
using FoltDelivery.Infrastructure.Persistance;
using FoltDelivery.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using FoltDelivery.Infrastructure.Tracing;
using FoltDelivery.Infrastructure.Tracing.Causation;
using FoltDelivery.Infrastructure.Tracing.Correlation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;

namespace FoltDelivery
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        AsyncPolicy? asyncPolicy = null;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();

            var eventStoreConnection = EventStoreConnection.Create(
                connectionString: Configuration.GetValue<string>("EventStore:ConnectionString"),
                builder: ConnectionSettings.Create().KeepReconnecting(),
                connectionName: Configuration.GetValue<string>("EventStore:ConnectionName"));

            eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();
      
            services.AddSingleton<ITracingScopeFactory, TracingScopeFactory>();

            services.AddSingleton(eventStoreConnection);
        
            services.AddScoped<IDataAccess, DataAccess>();
            AddTracing(services);
            services.AddSingleton(sp => new EventBus(
           sp,
           sp.GetRequiredService<ITracingScopeFactory>().CreateTraceScope,
           asyncPolicy ?? Policy.NoOpAsync()
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
                //.AllowCredentials();
            }));

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoltDelivery", Version = "v1" });
            });

            services.AddDbContext<FoltDeliveryDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("FoltAppCon"),
                    assembly => assembly.MigrationsAssembly(typeof(FoltDeliveryDbContext).Assembly.FullName)).UseLazyLoadingProxies();
                //assembly => assembly.MigrationsAssembly(typeof(HospitalDbContext).Assembly.FullName));
            });

            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddTransient<IEventHandler<OrderPlaced>, OrderHandler>();
            //services.AddTransient<IEventHandler<OrderCreated>, OrderHandler>();

            services.AddScoped<IEventStore, Infrastructure.EventStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoltDelivery v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static IServiceCollection AddTracing( IServiceCollection services)
        {
            services.TryAddSingleton<ICorrelationIdFactory, GuidCorrelationIdFactory>();
            services.TryAddSingleton<ICausationIdFactory, GuidCausationIdFactory>();
            services.TryAddScoped<ICorrelationIdProvider, CorrelationIdProvider>();
            services.TryAddScoped<ICausationIdProvider, CausationIdProvider>();
            services.TryAddScoped<ITraceMetadataProvider, TraceMetadataProvider>();
            services.TryAddSingleton<ITracingScopeFactory, TracingScopeFactory>();

            services.TryAddScoped<Func<IServiceProvider, TraceMetadata?, TracingScope>>(sp =>
                (scopedServiceProvider, traceMetadata) =>
                    sp.GetRequiredService<ITracingScopeFactory>().CreateTraceScope(scopedServiceProvider, traceMetadata)
            );

            services.TryAddScoped<Func<IServiceProvider, IEventEnvelope?, TracingScope>>(sp =>
                (scopedServiceProvider, eventEnvelope) => sp.GetRequiredService<ITracingScopeFactory>()
                    .CreateTraceScope(scopedServiceProvider, eventEnvelope)
            );

            return services;
        }
    }
}
