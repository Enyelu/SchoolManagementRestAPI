using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace SchoolMgtAPI.ExtensionMethods
{
    public static class JWTExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = env.IsDevelopment() ? configuration["JwtSettings:Audience"] : configuration["HerokuJWTSettings:HerokuAudience"],
                ValidIssuer = env.IsDevelopment() ? configuration["JwtSettings:Issuer"] : configuration["HerokuJWTSettings:HerokuIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(env.IsDevelopment() ?  configuration["JwtSettings:SecretKey"] : configuration["JwtSettings:HerokuSecretKey"])),
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.TokenValidationParameters = tokenValidationParameters;
           });
        }
    }
}