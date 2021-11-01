using Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace Utilities.AppUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepo Admin { get; }
        IClassAdviserRepo ClassAdviser { get; }
        ICourseRepo Course { get; }
        IDepartmentRepo Department { get; }
        INonAcademicStaffRepo NonAcademicStaff { get; }
        IStudentRepo Student { get; }
        ILecturerRepo Lecturer { get; }
        IFacultyRepo Faculty { get; }
        Task SaveChangesAsync();
    }
}
