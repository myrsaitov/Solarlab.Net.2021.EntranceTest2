
using WidePictBoard.Application.Comment.Implementations;
using WidePictBoard.Application.Comment.Interfaces;
using WidePictBoard.Application.User.Implementations;
using WidePictBoard.Application.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using WidePictBoard.Application.Content.Implementations;
using WidePictBoard.Application.Content.Interfaces;

namespace Advertisement.Infrastructure
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserServiceV1>();
            services.AddScoped<IContentService, ContentServiceV1>();
            services.AddScoped<ICommentService, CommentServiceV1>();

            return services;
        }
    }
}