
using Mapster;
using MapsterMapper;
using WidePictBoard.Infrastructure;
using WidePictBoard.Infrastructure.DataAccess;
using WidePictBoard.API.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WidePictBoard.Application.MapProfiles;


namespace WidePictBoard.API
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

                    //configuration.InMemory()
                    configuration.InSqlServer(Configuration.GetConnectionString("SqlServerDb"))
                //configuration.InPostgress(Configuration.GetConnectionString("PostgresDb"))
                )
                .AddIdentity(Configuration);

            services.AddSwaggerModule();

            //services.AddAutomapperModule();


            //Mapster
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