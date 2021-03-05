using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using WidePictBoard.Application;



namespace WidePictBoard.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddScoped<ITokenGenerator, JwtTokenGenerator>()
                .AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();
            
            return services;
        }
    }
}