using System;
using System.Collections.Specialized;
using Advertisement.PublicApi.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WidePictBoard.API.Controllers;

namespace WidePictBoard.PublicApi.Controllers
{
    public static class ApplicationExceptionExtensions
    {
        public static void UseApplicationException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApplicationExceptionHandler>();
        }

        public static void AddApplicationException(this IServiceCollection services,
            Action<ApplicationExceptionOptions> setupAction = null)
        {
            services.Configure(setupAction);
        }
    }
}