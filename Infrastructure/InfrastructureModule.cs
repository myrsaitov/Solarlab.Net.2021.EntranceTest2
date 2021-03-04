using WidePictBoard.Application.User.Interfaces;
using WidePictBoard.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;


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