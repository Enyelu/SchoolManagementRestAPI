using System;
using Utilities.GeneralResponse;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Interfaces
{
    public interface IAppUserService
    {
        Task<Response<string>> ConfirmEmailAsync(string userId, string token);
        Task<Response<string>> ForgotPasswordAsync(string email);
        Task<Response<string>> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    }
}
