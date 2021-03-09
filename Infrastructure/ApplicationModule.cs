using WidePictBoard.Application.Services.Content.Implementations;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Application.Services.User.Implementations;
using WidePictBoard.Application.Services.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WidePictBoard.Infrastructure
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //services.AddScoped<IContentService, ContentServiceV1>();
            services.AddScoped<IUserService, UserServiceV1>();

            return services;
        }

    }
}