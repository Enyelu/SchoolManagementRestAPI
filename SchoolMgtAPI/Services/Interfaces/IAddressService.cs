using System.Threading.Tasks;
using Utilities.Dtos;

namespace Services.Interfaces
{
    public interface IAddressService 
    {
        Task UpdateAddressAsync(UpdateAddressDto addressDto, string userId);
    }
}
