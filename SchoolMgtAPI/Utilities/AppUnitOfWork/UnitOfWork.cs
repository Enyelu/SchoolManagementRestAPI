using Data;
using Repository.Implementations;
using Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace Utilities.AppUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAdminRepo _admin;
        private IClassAdviserRepo _classAdviser;
        private ICourseRepo _course;
        private IDepartmentRepo _department;
        private INonAcademicStaffRepo _nonAcademicStaff;
        private IFacultyRepo _faculty;
        IStudentRepo _student;
        ILecturerRepo _lecturer;

        private readonly SchoolDbContext _context;

        public UnitOfWork(SchoolDbContext context)
        {
            _context = context;
        }

        public IAdminRepo Admin => _admin ??= new AdminRepo(_context);
        public IClassAdviserRepo ClassAdviser => _classAdviser ??= new ClassAdviserRepo(_context);
        public ICourseRepo Course => _course ??= new CourseRepo(_context);
        public IDepartmentRepo Department => _department ??= new DepartmentRepo(_context);
        public INonAcademicStaffRepo NonAcademicStaff => _nonAcademicStaff ??= new NonAcademicStaffRepo(_context);
        public IStudentRepo Student => _student ??= new StudentRepo(_context);
        public ILecturerRepo Lecturer => _lecturer ??= new LecturerRepo(_context);
        public IFacultyRepo Faculty => _faculty ??= new FacultyRepo(_context);

        public async Task SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
