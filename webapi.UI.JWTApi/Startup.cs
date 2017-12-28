using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webapi.Infrastructure.Services.ADMS.Core.Services;
using webapi.core.Data;
using Microsoft.EntityFrameworkCore;
using webapi.core.Interface;
using webapi.Infrastructure.Services.Interface;
using Swashbuckle.AspNetCore.Swagger;

namespace webapi.UI.JWTApi
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
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ExamConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(UserRepository<>));
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(UserRepository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserService>();
            services.AddSingleton(new LoggerFactory());           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(cfg =>
               {
                   cfg.RequireHttpsMetadata = false;
                   cfg.SaveToken = true;

                   cfg.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidIssuer = Configuration["JwtSecurityToken:Issuer"],
                       ValidAudience = Configuration["JwtSecurityToken:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityToken:key"])) //Secret
                    };

               });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "JWT API", Version = "v1" });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                                name: "DefaultApi",
                                template: "api/{controller}/{id}",
                                defaults: new { controller = "values", action = "GetAll" });
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Exam Token Authentication");
            });
            app.UseMvc();
        }
    }
}
