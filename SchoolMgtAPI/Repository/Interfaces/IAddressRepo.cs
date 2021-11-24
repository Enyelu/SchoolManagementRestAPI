using Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public  interface IAddressRepo : IGenericRepository<Address>
    {
        Task<Address> GetAddressAsync(string addressId);
    }
}
