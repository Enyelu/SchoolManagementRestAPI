using Data;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class AdminRepo : IAdminRepo
    {
        private readonly SchoolDbContext _context;  
        public AdminRepo(SchoolDbContext context)
        {
            _context = context;
        }
    }
}
