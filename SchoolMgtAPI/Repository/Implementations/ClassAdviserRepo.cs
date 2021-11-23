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
            return await _context.ClassAdvisersers.FirstOrDefaultAsync(x => x.Level == level && x.Lecturer.Department.Name.Trim().ToLower() == department.Trim().ToLower());
        }

    }
}
