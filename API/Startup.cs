using MapsterMapper;
using SL2021.Infrastructure;
using SL2021.Infrastructure.DataAccess;
using SL2021.API.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SL2021.Application.MapProfiles;


namespace SL2021.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services
                .AddCors()
                .AddApplicationModule(Configuration)
                .AddHttpContextAccessor()
                .AddDataAccessModule(configuration =>

                  
                    configuration.InSqlServer(Configuration.GetConnectionString("SqlServerDb"))
               
                )
                .AddIdentity(Configuration);

            services.AddSwaggerModule();

           //Mapster
            services.AddSingleton(CategoryMapProfile.GetConfiguredMappingConfig());
            services.AddSingleton(CommentMapProfile.GetConfiguredMappingConfig());
            services.AddSingleton(ContentMapProfile.GetConfiguredMappingConfig());
            services.AddSingleton(UserMapProfile.GetConfiguredMappingConfig());
            
            services.AddScoped<IMapper, ServiceMapper>();
            
            services.AddApplicationException(config => { config.DefaultErrorStatusCode = 500; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Init migrations
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.Migrate();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PublicApi v1"));
            app.UseApplicationException();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}