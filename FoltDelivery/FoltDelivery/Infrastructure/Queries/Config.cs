using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Reflection;

namespace FoltDelivery.Infrastructure.Queries
{
    public static class Config
    {
        public static IServiceCollection AddQueryHandler<TQuery, TQueryResult, TQueryHandler>(
            this IServiceCollection services
        )
            where TQuery : IQuery<TQueryResult>
            where TQueryHandler : class, IQueryHandler<TQuery, TQueryResult>
        {
            return services.AddTransient<TQueryHandler>()
                .AddTransient<IRequestHandler<TQuery, TQueryResult>>(sp => sp.GetRequiredService<TQueryHandler>())
                .AddTransient<IQueryHandler<TQuery, TQueryResult>>(sp => sp.GetRequiredService<TQueryHandler>());
        }

        public static IServiceCollection AddAllQueryHandlers(
             this IServiceCollection services,
             ServiceLifetime withLifetime = ServiceLifetime.Transient)
        {
            return services.Scan(scan => scan
                .FromAssemblies(System.AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(classes =>
                    classes.AssignableTo(typeof(IQueryHandler<,>))
                        .Where(c => !c.IsAbstract && !c.IsGenericTypeDefinition))
                .AsSelfWithInterfaces()
                .WithLifetime(withLifetime)
            );
        }


    }
}
