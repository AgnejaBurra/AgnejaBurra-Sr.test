using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApi.BussinessLogic.BusinessLogic;
using CoreApi.BussinessLogic.IBusinessLogic;
using CoreApi.DataAccess.IRepositories;
using CoreApi.DataAccess.Repositories;
using CoreApi.Infrastructure.Models;
using CoreApi.WebAPI.AutoMapper;
using FluentValidation;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreApi.WebAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// Configuration
        /// </summary>
        private IConfiguration Configuration { get; }
        private IServiceCollection _services;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigureAutoMapper.Configure();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Synagro RMS API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                                    { "Bearer", Enumerable.Empty<string>() },
                    });
            });

            //Register CORS service and policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    poll =>
                    {
                        poll.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            //.AllowCredentials()
                            .WithOrigins()
                            .WithExposedHeaders("x-custom-header");
                    });
            });
            #endregion
            //Add AutoMapper Service
            services.AddHttpContextAccessor();
            #region Custom businesslogic and repository logic dependency injection    

                services.AddTransient<IEmployeeLogic, EmployeeLogic>();
                services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseRequestLocalization();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Synagro RMS API V1");

            });
            logger.LogInformation("startup configuration done");

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
        }


        //Update database on application start from latest migration files
        
    }
}
