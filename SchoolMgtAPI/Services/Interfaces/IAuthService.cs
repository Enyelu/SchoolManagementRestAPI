using Models;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response<string>> ConfirmEmailAsync(string email, string token);
        Task<Response<LoginResponseDto>> Login(LoginDto loginDto);
        Task<Response<string>> ForgotPassword(string email);
        Task<Response<string>> ResetPassword(ResetPasswordModel passwordRequest);
        Task<Response<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestDto tokenRequestDto);
    }
}