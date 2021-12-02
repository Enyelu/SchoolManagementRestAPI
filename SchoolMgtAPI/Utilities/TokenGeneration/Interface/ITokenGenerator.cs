using Models;
using System.Threading.Tasks;

namespace Utilities.Interface.TokenGeneration
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(AppUser appUser);
        string GenerateRefreshToken();
    }
}
