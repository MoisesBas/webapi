using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapi.core.Interface;
using webapi.core.Data;
using webapi.Infrastructure.Services.Interface;
using webapi.Infrastructure.Services.ADMS.Core.Services;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace webapi.UI.Users
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
            
            services.AddScoped(typeof(IRepository<>), typeof(UserRepository<>));
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(UserRepository<>));
            services.AddScoped<IUserService, UserService>();

            // services.AddSwaggerGen(options => {
            //     options.SwaggerDoc("V1", new Info { Title = "API Sample Authentication", Version ="1.0.0.20" });
            //    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //    var fullPath = Path.Combine(basePath, "webapi.xml");
            //    options.IncludeXmlComments(fullPath);
            //});
            //services.AddMvc();
            ///*Adding swagger generation with default settings*/           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    //app.UseAuthentication();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c =>
            //    {
            //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1");
            //    });
            //    app.UseDeveloperExceptionPage();
            //}
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //                    name: "users",
            //                    template: "login/{user:string.empty}",
            //                    defaults: new { controller = "users", action = "login" });
            //});            
        }
    }
}
