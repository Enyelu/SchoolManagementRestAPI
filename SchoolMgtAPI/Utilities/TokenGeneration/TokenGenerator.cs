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

namespace Utilities.TokenGeneration
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
            Audience = env.IsDevelopment() ? _configuration.GetSection("Audience").Value : _configuration.GetSection("HerokuAudience").Value;
            Issuer = env.IsDevelopment() ? _configuration.GetSection("Issuer").Value : _configuration.GetSection("HerokuIssuer").Value;
            SecretKey = env.IsDevelopment() ? _configuration.GetSection("SecretKey").Value : _configuration.GetSection("HerokuSecretKey").Value;
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

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var token = new JwtSecurityToken
                (
                    audience: Audience,
                    issuer: Issuer,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                    expires: DateTime.Now.AddMinutes(30),
                    claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}