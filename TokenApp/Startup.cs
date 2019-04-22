using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// для встраивания функциональности JWT-токенов
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens;
using TokenApp.Models;

namespace TokenApp
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
            #region Настройка приложения на работу с JWT-токенами
            //настраиваем приложение на работу с токенами 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // НА БОЮ ВКЛЮЧАТЬ!!!
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //будем ли проверять издателя токена
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,

                        //будет ли валидироваться потребитель токена (то приложение к которому идет подключение)
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,

                        //будет ли валидироваться время существования
                        ValidateLifetime = true,

                        //устанавливаем ключ безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        //валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
