using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class AddressRepo : GenericRepository<Address>, IAddressRepo
    {
        private readonly SchoolDbContext _context;
        public AddressRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Address> GetAddressAsync(string addressId)
        {
           return await _context.Addresses.FirstOrDefaultAsync(x => x.Id == addressId);
        }
    }
}