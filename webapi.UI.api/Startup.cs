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
using Swashbuckle.AspNetCore.Swagger;
using webapi.core.Interface;
using webapi.core.Data;
using webapi.Infrastructure.Services.Interface;
using webapi.Infrastructure.Services.ADMS.Core.Services;
using webapi.core.Mapping;

using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using webapi.UI.api.Helper;
using System.Text;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using webapi.core.Migrations;

namespace webapi.UI.api
{
    public class Startup
    {
        private IServiceCollection _services;
        
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
            // add logging
            services.AddSingleton(new LoggerFactory()
            .AddConsole()
            .AddDebug());
            services.AddLogging();            
            services.AddTransient<UserSeedData>();
            //services.AddTransient<UserMigrationConfiguration>();
            services.AddMvc();
            
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My Exam", Version = "v1" });
            });
           

           
            services.AddMvc().AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options => {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = Configuration["JwtSecurityToken:Issuer"],
                           ValidAudience = Configuration["JwtSecurityToken:Audience"],
                           IssuerSigningKey = apiSecurityKey.Create(Configuration["JwtSecurityToken:key"])
                       };

                       options.Events = new JwtBearerEvents
                       {
                           OnAuthenticationFailed = context =>
                           {
                               Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                               return Task.CompletedTask;
                           },
                           OnTokenValidated = context =>
                           {
                               Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                               return Task.CompletedTask;
                           }
                       };                      
                   });           

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member",
                    policy => policy.RequireClaim("MembershipId"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserMigrationConfiguration config)
         public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserSeedData userdata)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
            }

           
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Exam OAuth");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                                name: "DefaultApi",
                                template: "api/{controller}/{id}",
                                defaults: new { controller = "users", action = "GetAll" });
            });
            app.UseMvc();
            //userdata.SeedData().Wait();

        }
    }
}
