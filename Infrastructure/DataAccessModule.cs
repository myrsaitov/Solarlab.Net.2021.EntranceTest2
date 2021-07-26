using System;
using SL2021.Application.Repositories;
using SL2021.Infrastructure.DataAccess;
using SL2021.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SL2021.Infrastructure
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


        public static void InSqlServer(this ModuleConfiguration moduleConfiguration, string connectionString)
        {
            moduleConfiguration.Services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString, builder =>
                    builder.MigrationsAssembly(
                        typeof(DataAccessModule).Assembly.FullName)
                        );
            });

            moduleConfiguration.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            moduleConfiguration.Services.AddScoped<IContentRepository, ContentRepository>();
            moduleConfiguration.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            moduleConfiguration.Services.AddScoped<ICommentRepository, CommentRepository>();
            moduleConfiguration.Services.AddScoped<IUserRepository, UserRepository>();
            moduleConfiguration.Services.AddScoped<ITagRepository, TagRepository>();
            moduleConfiguration.Services.AddScoped<IImageRepository, ImageRepository>();
            moduleConfiguration.Services.AddScoped<IUserPicRepository, UserPicRepository>();

        }
    }
}