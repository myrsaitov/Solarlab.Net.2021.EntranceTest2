using SL2021.Application.Services.Content.Implementations;
using SL2021.Application.Services.Content.Interfaces;
using SL2021.Application.Services.Mail.Interfaces;
using SL2021.Application.Services.User.Implementations;
using SL2021.Application.Services.User.Interfaces;
using SL2021.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SL2021.Application.Services.Tag.Implementations;
using SL2021.Application.Services.Tag.Interfaces;
using SL2021.Application.Services.Category.Implementations;
using SL2021.Application.Services.Category.Interfaces;
using SL2021.Application.Services.Comment.Implementations;
using SL2021.Application.Services.Comment.Interfaces;
using SL2021.Application.Services.Image.Implementations;
using SL2021.Application.Services.Image.Interfaces;
using SL2021.Application.Services.UserPic.Implementations;
using SL2021.Application.Services.UserPic.Interfaces;
using SL2021.Application.Services.WebLink.Implementations;
using SL2021.Application.Services.WebLink.Interfaces;

namespace SL2021.Infrastructure
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
            services.AddScoped<IImageService, ImageServiceV1>();
            services.AddScoped<IUserPicService, UserPicServiceV1>();
            services.AddScoped<IWebLinkService, WebLinkServiceV1>();

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailServiceMock>();

            return services;
        }
    }
}