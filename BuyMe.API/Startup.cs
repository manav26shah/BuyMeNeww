using BuyMe.API.Config;
using BuyMe.BL;
using BuyMe.BL.Implementation;
using BuyMe.BL.Interface;
using BuyMe.DL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        // Dependecy Injection- Design pattern , creational Design Pattern
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AddSwagger(services);
            RegisterDbServices(services);
            RegisterBusinessServces(services);
            RegisterConfigurations(services);

        }

        public void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen();
        }
        public void RegisterBusinessServces(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
        public void RegisterConfigurations(IServiceCollection services)
        {
            services.Configure<EmailConfig>(Configuration.GetSection("Email"));
            services.Configure<AuthConfig>(Configuration.GetSection("Auth"));
        }

        public void RegisterDbServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConectionString"));
            });

            services.AddScoped<IRepo, Repo>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting(); // this middleware decides which action method from which controler call, what value to apss, in the querystring 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }

        private RequestDelegate SomeMethod(RequestDelegate context)
        {
            return context;
        }
    }
}
