using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WidePictBoard.Infrastructure
{
    public static class AutomapperModule
    {
        public static IServiceCollection AddAutomapperModule(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
            return services;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<WidePictBoard.Application.MapProfiles.UserMapProfile>();
            });

            configuration.AssertConfigurationIsValid();

            return configuration;
        }
    }
}