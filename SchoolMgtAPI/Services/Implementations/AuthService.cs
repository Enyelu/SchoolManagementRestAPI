using Services.Interfaces;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordService _passwordService;

        public AuthService(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }
        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var respose = await _passwordService.ConfirmEmailAsync(email, token);

            if (respose.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
