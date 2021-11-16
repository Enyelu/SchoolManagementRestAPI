using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class NonAcademicStaffPositionRepo : GenericRepository<NonAcademicStaffPosition> , INonAcademicStaffPositionRepo
    {
        private readonly SchoolDbContext _context;
        public NonAcademicStaffPositionRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<NonAcademicStaffPosition> GetNonAcademicStaffPositionAsync(string positionName)
        {
            return await _context.NonAcademicStaffPositions.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == positionName.ToLower().Trim());
        }
    }
}
