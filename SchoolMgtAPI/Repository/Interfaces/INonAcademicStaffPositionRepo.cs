using Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface INonAcademicStaffPositionRepo : IGenericRepository<NonAcademicStaffPosition>
    {
        Task<NonAcademicStaffPosition> GetNonAcademicStaffPositionAsync(string positionName);
    }
}
