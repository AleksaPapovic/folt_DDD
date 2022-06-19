using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.Client;
using EventStore.ClientAPI;
using FoltDelivery.Infrastructure;
using FoltDelivery.Model;
using FoltDelivery.Repository;
using FoltDelivery.Service;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore.Proxies;

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

            services.AddControllers();

            var eventStoreConnection = EventStoreConnection.Create(
                connectionString: Configuration.GetValue<string>("EventStore:ConnectionString"),
                builder: ConnectionSettings.Create().KeepReconnecting(),
                connectionName: Configuration.GetValue<string>("EventStore:ConnectionName"));

            eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();

            services.AddSingleton(eventStoreConnection);




            //services.AddAuthentication(auth =>
            //    {
            //        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //        auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.SaveToken = true;
            //        options.RequireHttpsMetadata = false;
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidAudience = Configuration["JwtToken:Audience"],
            //            ValidIssuer = Configuration["JwtToken:Issuer"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtToken:SigningKey"]))
            //        };
            //    });
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Patient", policy => policy.Requirements.Add(new RoleRequirement("patient")));
                //options.AddPolicy("Manager", policy => policy.Requirements.Add(new RoleRequirement("manager")));
            });




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


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventStore, GetEventStore>();
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

        static EventStoreClient ConfigureEventStore(string connectionString, ILoggerFactory loggerFactory)
        {
            var settings = EventStoreClientSettings.Create(connectionString);
            settings.ConnectionName = "bookingApp";
            settings.LoggerFactory = loggerFactory;
            return new EventStoreClient(settings);
        }
    }
}
