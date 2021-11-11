using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        public AppUserService(UserManager<AppUser> userManager, IConfiguration configuration, IMailService mailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
        }
        public async Task<Response<string>> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var emailTokenDecoded = WebEncoders.Base64UrlDecode(token);
                var validEmailTokenDecoded = Encoding.UTF8.GetString(emailTokenDecoded);
                var response = await _userManager.ConfirmEmailAsync(user, validEmailTokenDecoded);

                if(response.Succeeded)
                {
                    return Response<string>.Success(null, "Email confirmation was successful");
                }
                return Response<string>.Fail("Email confirmation was unsuccessful. Try again");
            }
            return Response<string>.Fail("User not found");
        }

        public async Task<Response<string>> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedRestPasswordToken = Encoding.UTF8.GetBytes(resetPasswordToken);
                var validResetPasswordToken = WebEncoders.Base64UrlEncode(encodedRestPasswordToken);

                string url = $"{_configuration.GetSection("applicationURL").Value}/ResetPassword?email={email}&token={validResetPasswordToken}";
                var sendMail = await _mailService.SendMailAsync(email, "<h1>Reset Password</h1>", $"<p> Dear {user.FirstName}, to reset your password, <a href='{url}'>click here</a></p>");

                return Response<string>.Success(null, $"Visit {email} to confirm your password reset");
            }
            return Response<string>.Fail($"{email} not a registered email");
        }
        public async Task<Response<string>> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
           var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);

            if(user != null)
            {
                if(resetPasswordModel.NewPassword == resetPasswordModel.ConfirmNewPassword)
                {
                    await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
                    return Response<string>.Success(null, "Password reset was successful");
                }
                return Response<string>.Fail("Password reset was unsuccessful. password and confirm password mismatch");
            }
            return  Response<string>.Fail("Password reset was unsuccessful. User not found");
        }
    }
}
