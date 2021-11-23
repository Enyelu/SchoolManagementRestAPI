using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class FacultyRepo : GenericRepository<Faculty>, IFacultyRepo
    {
        private readonly SchoolDbContext _context;
        public FacultyRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Faculty> GetFacultyAsync(string facultyName)
        {
          return  await _context.Faculties
                    .Include(x => x.Courses)
                    .ThenInclude(x => x.Department)
                    .ThenInclude(x => x.Lecturer)
                    .ThenInclude(x => x.AppUser)
                    .ThenInclude(x => x.Student)
                    .Include(x => x.Students)
                    .Include(x => x.Lecturer)
                    .Include(x => x.Departments)
                    .Include(x => x.NonAcademicStaff)
                    .FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == facultyName.Trim().ToLower());
        }
    }
}
