using Models;
using System.Threading.Tasks;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IPasswordService
    {
        Task<Response<string>> ConfirmEmailAsync(string email, string token);
        Task<Response<string>> ForgotPasswordAsync(string email);
        Task<Response<string>> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    }
}
