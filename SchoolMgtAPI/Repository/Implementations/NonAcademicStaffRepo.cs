using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class NonAcademicStaffRepo : INonAcademicStaffRepo
    {
        private readonly SchoolDbContext _context;
        public NonAcademicStaffRepo(SchoolDbContext context)
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
                                      .Include(x => x.DutyPost).ToListAsync();
            return staff;
        }

        public async Task<bool> DeactivateNonAcademicStaffAsync(string staffId)
        {
            var staff = await GetNonAcademicStaffAsync(staffId);
            if(staff != null)
            {
                var response = staff.AppUser.IsActive = false;
                return response;
            }
            return false;
        }

        public bool UpdateNonAcademicStaff(NonAcademicStaff staff)
        {
           var response = _context.NonAcademicStaff.Update(staff);
            if(response.State > 0)
            {
                return true;
            }
            return false;
        }
    }
}
