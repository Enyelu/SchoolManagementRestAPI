using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class DepartmentRepo : GenericRepository<Department>, IDepartmentRepo
    {
        private readonly SchoolDbContext _context;

        public DepartmentRepo(SchoolDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<Department> GetDepartmentAsync(string departmentName)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == departmentName.ToLower().Trim());
            
            if (department != null)
            {
                return department;
            }
            return null;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentDetailsAsync()
        {
            return await _context.Departments.Include(x => x.Lecturer)
                                             .Include(x => x.NonAcademicStaff)
                                             .Include(x => x.Courses)
                                             .Include(x => x.Faculty)
                                             .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentAsync()
        {
            var departments =  await _context.Departments.ToListAsync();
            if (departments != null)
            {
                return departments;
            }
            return null;
        }
    }
}
