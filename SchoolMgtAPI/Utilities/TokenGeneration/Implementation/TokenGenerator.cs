using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities.Interface.TokenGeneration;

namespace Utilities.Implementation.TokenGeneration
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly string Audience;
        private readonly string Issuer;
        private readonly string SecretKey;

        public TokenGenerator(UserManager<AppUser> userManager, IConfiguration configuration, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _configuration = configuration;
            Audience = env.IsDevelopment() ? _configuration["JwtSettings:Audience"] : _configuration["JwtSettings:HerokuAudience"];
            Issuer = env.IsDevelopment() ? _configuration["JwtSettings:Issuer"] : _configuration["JwtSettings:HerokuIssuer"];
            SecretKey = env.IsDevelopment() ? _configuration["JwtSettings:SecretKey"] : _configuration["HerokuSecretKey:HerokuSecretKey"];
        }
        
        public async Task<string> GenerateTokenAsync(AppUser appUser)
        {
            
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(ClaimTypes.MobilePhone, appUser.PhoneNumber),
                new Claim(ClaimTypes.Name, $"{appUser.FirstName} {appUser.LastName}"),
                new Claim(ClaimTypes.GivenName, appUser.UserName),
            };

            var roles = await _userManager.GetRolesAsync(appUser);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var securityKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var token = new JwtSecurityToken
                (
                    audience: Audience,
                    issuer: Issuer,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                    expires: DateTime.Now.AddHours(1),
                    claims: claims
                );
            
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}