using System;
using LandmarkRemark.API.Database;
using LandmarkRemark.API.Database.Repositories;
using LandmarkRemark.API.Services;
using LandmarkRemark.API.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace LandmarkRemark.API
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
            services.AddSingleton<IClock, Clock>();
            ConfigureRepositories(services);
            ConfigureApplicationServices(services);
            
            EnableValidateModel(services);

            services.AddCors(options =>
            {
                //allow requests from  
                options.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LandmarkRemark.API", Version = "v1" });
            });
        }

        private static void EnableValidateModel(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<DatabaseContext>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LandmarkRemark.API v1"));
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseMiddleware(typeof(ErrorMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
