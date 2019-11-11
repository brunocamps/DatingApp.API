using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) //configure services method. Dependency injection
        {
            //available to the injective and any other part of the application
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); //install sqlite
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(); //make CORS available to use its middleware
            services.AddScoped<IAuthRepository, AuthRepository>(); //lec 28
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes
                   (Configuration.GetSection("AppSettings:Token").Value)),
                   ValidateIssuer = false,
                   ValidateAudience = false

                };
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //developer friendly exception page
            }
            else
            {
                //app.UseHsts(); //security enhancement. 
            }

            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc(); //Middleware: routes requests to correct controller. 
            //Middleware: connects. Sits between client requests' and our API endpoint.
            //Router the request to the particular controller.
        }
    }
}
