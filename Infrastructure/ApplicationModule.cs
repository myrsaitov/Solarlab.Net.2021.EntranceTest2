using WidePictBoard.Application.Services.Content.Implementations;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Application.Services.Mail.Interfaces;
using WidePictBoard.Application.Services.User.Implementations;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WidePictBoard.Application.Services.Category.Implementations;
using WidePictBoard.Application.Services.Category.Interfaces;

namespace WidePictBoard.Infrastructure
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryServiceV1>();
            services.AddScoped<IContentService, CommentServiceV1>();
            services.AddScoped<IContentService, ContentServiceV1>();
            services.AddScoped<IUserService, UserServiceV1>();

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            //services.AddScoped<IMailService, MailService>();
            services.AddScoped<IMailService, MailServiceMock>();

            return services;
        }

    }
}