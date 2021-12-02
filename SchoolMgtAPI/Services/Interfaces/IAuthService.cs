using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response<string>> ConfirmEmailAsync(string email, string token);
        Task<Response<LoginResponseDto>> Login(LoginDto loginDto);
        Task<Response<string>> ForgotPassword(EmailRequestDto passwordRequest);
        Task<Response<string>> ResetPassword(ResetPasswordModel passwordRequest);
        Task<Response<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestDto tokenRequestDto);
    }
}