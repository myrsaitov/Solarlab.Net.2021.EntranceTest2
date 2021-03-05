﻿
using WidePictBoard.Application.Services.Comment.Implementations;
using WidePictBoard.Application.Services.Comment.Interfaces;
using WidePictBoard.Application.Services.User.Implementations;
using WidePictBoard.Application.Services.User.Interfaces;
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