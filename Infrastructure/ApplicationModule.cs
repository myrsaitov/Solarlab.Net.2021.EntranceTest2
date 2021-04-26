using WidePictBoard.Application.Services.Content.Implementations;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Application.Services.Mail.Interfaces;
using WidePictBoard.Application.Services.User.Implementations;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WidePictBoard.Application.Services.Tag.Implementations;
using WidePictBoard.Application.Services.Tag.Interfaces;
using WidePictBoard.Application.Services.Category.Implementations;
using WidePictBoard.Application.Services.Category.Interfaces;
using WidePictBoard.Application.Services.Comment.Implementations;
using WidePictBoard.Application.Services.Comment.Interfaces;

namespace WidePictBoard.Infrastructure
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryServiceV1>();
            services.AddScoped<ICommentService, CommentServiceV1>();
            services.AddScoped<IContentService, ContentServiceV1>();
            services.AddScoped<ITagService, TagServiceV1>();
            services.AddScoped<IUserService, UserServiceV1>();

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailServiceMock>();

            return services;
        }
    }
}