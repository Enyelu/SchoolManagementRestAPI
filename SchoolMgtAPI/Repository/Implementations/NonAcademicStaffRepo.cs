using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class NonAcademicStaffRepo : GenericRepository<NonAcademicStaff>, INonAcademicStaffRepo
    {
        private readonly SchoolDbContext _context;
        public NonAcademicStaffRepo(SchoolDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<NonAcademicStaff> GetNonAcademicStaffAsync(string staffId)
        {
            var staff = await _context.NonAcademicStaff
                    .Include(x => x.AppUser)
                    .Include(x => x.DutyPost).FirstOrDefaultAsync(x => x.AppUserId == staffId);
            return staff;
        }

        public async Task<IEnumerable<NonAcademicStaff>> GetAllNonAcademicStaffAsync()
        {
            var staff = await _context.NonAcademicStaff
                                      .Include(x => x.AppUser)
                                      .Include(x => x.DutyPost).Where(x => x.AppUser.IsActive == true).ToListAsync();
            return staff;
        }

    }
}
