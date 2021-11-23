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
            var department = await _context.Departments
                                           .Include(x => x.Courses)
                                           .ThenInclude(x => x.Lecturers)
                                           .ThenInclude(x => x.AppUser)
                                           .ThenInclude(x => x.Address)
                                           .Include(x => x.NonAcademicStaff)
                                           .ThenInclude(x => x.AppUser)
                                           .ThenInclude(x => x.Address)
                                           .Include(x => x.Lecturer)
                                           .ThenInclude(x => x.AppUser)
                                           .ThenInclude(x => x.Address)
                                           .Include(x => x.Faculty)
                                           .ThenInclude(x => x.Departments)
                                           .FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == departmentName.ToLower().Trim());
            
            if (department != null)
            {
                return department;
            }
            return null;
        } 

        public async Task<IEnumerable<Department>> GetAllDepartmentDetailsAsync()
        {
            return await _context.Departments.Include(x => x.Lecturer)
                                             .ThenInclude(x => x.AppUser)
                                             .ThenInclude(x => x.Address)
                                             .Include(x => x.NonAcademicStaff)
                                             .ThenInclude(x => x.AppUser)
                                             .ThenInclude(x => x.Address)
                                             .Include(x => x.Courses)
                                             .ThenInclude(x => x.Students)
                                             .Include(x => x.Faculty)
                                             .ThenInclude(x => x.Lecturer)
                                             .ThenInclude(x => x.AppUser)
                                             .ThenInclude(x => x.Student)
                                             .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetActiveDepartmentAsync()
        {
            var departments =  await _context.Departments.Where(x => x.IsActive == true).ToListAsync();
            if (departments != null)
            {
                return departments;
            }
            return null;
        }
    }
}
