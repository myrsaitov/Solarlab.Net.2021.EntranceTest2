using System;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;
using WidePictBoard.Infrastructure.DataAccess;
using WidePictBoard.Infrastructure.DataAccess.Repositories;
//using Advertisement.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using InMemoryRepository = WidePictBoard.Infrastructure.DataAccess.Repositories.InMemoryRepository;

namespace WidePictBoard.Infrastructure
{
    public static class DataAccessModule
    {
        public sealed class ModuleConfiguration
        {
            public IServiceCollection Services { get; init; }
        }

        public static IServiceCollection AddDataAccessModule(
            this IServiceCollection services,
            Action<ModuleConfiguration> action
        )
        {
            var moduleConfiguration = new ModuleConfiguration
            {
                Services = services
            };
            action(moduleConfiguration);
            return services;
        }

       /* public static void InMemory(this ModuleConfiguration moduleConfiguration)
        {
            moduleConfiguration.Services.AddSingleton(new InMemoryRepository());
            moduleConfiguration.Services.AddSingleton<IRepository<User, string>>(sp =>
                sp.GetService<InMemoryRepository>());
            moduleConfiguration.Services.AddSingleton<IRepository<Content, int>>(sp => sp.GetService<InMemoryRepository>());
        }*/

        public static void InSqlServer(this ModuleConfiguration moduleConfiguration, string connectionString)
        {
            moduleConfiguration.Services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString, builder =>
                    builder.MigrationsAssembly(
                        typeof(DataAccessModule).Assembly.FullName)
                // typeof(DatabaseContextModelSnapshot).Assembly.FullName)
                );
            });

            moduleConfiguration.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            moduleConfiguration.Services.AddScoped<IContentRepository, ContentRepository>();
            moduleConfiguration.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        // public static void InPostgress(this ModuleConfiguration moduleConfiguration, string connectionString)
        // {
        // moduleConfiguration.Services.AddDbContextPool<DatabaseContext>(options =>
        // {
        //     options.UseNpgsql(connectionString, builder =>
        //         builder.MigrationsAssembly(
        //             //typeof( DataAccessModule).Assembly.FullName)
        //             //typeof(DatabaseContextModelSnapshot).Assembly.FullName)
        //     );
        // });
        //
        // moduleConfiguration.Services.AddScoped<IRepository<Ad, int>, EfRepository<Ad, int>>();
        // moduleConfiguration.Services.AddScoped<IRepository<User, int>, EfRepository<User, int>>();
        // }
    }
}