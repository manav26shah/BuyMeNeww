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
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuyMe.API.Middlewares;
using BuyMe.DL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using BuyMe.API.services;

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

            //
            services.AddScoped<IEmailSender, MockEmailSender>();

            // Register Identity
            services.AddIdentity<BuyMeUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().
                AddDefaultTokenProviders();

            AddJwtAuthentication(services);


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
            app.AddCustomHeader();
            app.UseHttpsRedirection();

            app.UseRouting(); // this middleware decides which action method from which controler call, what value to apss, in the querystring 

            app.UseCors(options =>
            {
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


        // Private methods
        // Custom methods for service regsitration
        private void AddJwtAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
          .AddJwtBearer(options =>
          {
              var secret = Configuration.GetSection("Jwt").GetValue<string>("Secret");
              options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
              {

                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                  ValidateAudience = false,
                  ValidateIssuer = false,
              };
              options.RequireHttpsMetadata = false;
          });
        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                options.IncludeXmlComments(path);

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name="Authorization",
                    Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme= JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat="JWT",
                    In=Microsoft.OpenApi.Models.ParameterLocation.Header
                });

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                var req = new OpenApiSecurityRequirement();
                req.Add(scheme, new List<string>());
                options.AddSecurityRequirement(req);
            });
        }
        private void RegisterBusinessServces(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
        }
        private void RegisterConfigurations(IServiceCollection services)
        {
            services.Configure<EmailConfig>(Configuration.GetSection("Email"));
            services.Configure<AuthConfig>(Configuration.GetSection("Auth"));
            services.Configure<JwtConfig>(Configuration.GetSection("Jwt"));
        }

        private void RegisterDbServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConectionString"));
            });

            services.AddScoped<IRepo, Repo>();
        }


    }
}
