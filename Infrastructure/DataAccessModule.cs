using System;
using WidePictBoard.Application;
using WidePictBoard.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Advertisement.Infrastructure
{
    public static class DataAccessModule
    {
        public sealed class Configurator
        {
            internal IServiceCollection Services { get; set; }
        }
        
        public static IServiceCollection AddDataAccessModule(
            this IServiceCollection services,
            Action<Configurator> configure)
        {
            var configurator = new Configurator
            {
                Services = services
            };
            configure(configurator);
            
            return services;
        }

        public static Configurator InMemory(this Configurator configurator)
        {
            configurator.Services
                .AddSingleton<InMemoryRepository>()
                .AddSingleton<IRepository<Domain.Comment, int>>(sp => sp.GetService<InMemoryRepository>())
                .AddSingleton<IRepository<Domain.Ad, int>>(sp => sp.GetService<InMemoryRepository>())
                .AddSingleton<IRepository<Domain.User, int>>(sp => sp.GetService<InMemoryRepository>());

            return configurator;
        }

        public static Configurator InSqlServer(this Configurator configurator, string connectionString)
        {
            throw new NotImplementedException();
            return configurator;
        }
    }
}