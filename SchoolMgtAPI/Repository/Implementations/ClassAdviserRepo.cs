﻿using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class ClassAdviserRepo : GenericRepository<ClassAdviser>, IClassAdviserRepo
    {
        private readonly SchoolDbContext _context;
        public ClassAdviserRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClassAdviser> GetClassAdviserByEmailAsync(string classAdviserEmail)
        {
            return await _context.ClassAdvisersers.FirstOrDefaultAsync(x => x.Lecturer.AppUser.Email == classAdviserEmail);
        }

        public async Task<ClassAdviser> GetClassAdviserByDepartmentAndLevelAsync(int level, string department)
        {
            return await _context.ClassAdvisersers
                                 .Include(x => x.Lecturer)
                                 .ThenInclude(x => x.AppUser)
                                 .Include(x => x.Lecturer)
                                 .ThenInclude(x => x.AppUser)
                                 .ThenInclude(x => x.Address)
                                 .Include(x => x.Lecturer)
                                 .ThenInclude(x => x.Department)
                                 .ThenInclude(x => x.Faculty)
                                 .Include(x => x.Students)
                                 .ThenInclude(x => x.AppUser)
                                 .ThenInclude(x => x.Address)
                                 .Where(x => x.IsCourseAdviser == true)
                                 .FirstOrDefaultAsync(x => x.Level == level && x.Lecturer.Department.Name.Trim().ToLower() == department.Trim().ToLower());
        }
    }
}
