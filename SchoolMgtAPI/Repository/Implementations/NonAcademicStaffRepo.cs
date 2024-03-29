﻿using Data;
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

        public async Task<NonAcademicStaff> GetNonAcademicStaffAsync(string staffEmail)
        {
            var staff = await _context.NonAcademicStaff
                    .Include(x => x.AppUser)
                    .Include(x => x.Department)
                    .Include(x => x.AppUser.Address)
                    .Include(x => x.Position)
                    .FirstOrDefaultAsync(x => x.AppUser.Email == staffEmail);
            return staff;
        }

        public async Task<IEnumerable<NonAcademicStaff>> GetAllNonAcademicStaffAsync()
        {
            var staff = await _context.NonAcademicStaff
                                      .Include(x => x.AppUser)
                                      .ThenInclude(x => x.Address)
                                      .Include(x => x.Position)
                                      .Include(x => x.Department)
                                      .Where(x => x.AppUser.IsActive == true).ToListAsync();
            return staff;
        }

    }
}
