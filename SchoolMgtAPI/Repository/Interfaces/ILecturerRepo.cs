using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ILecturerRepo :IGenericRepository<Lecturer>
    {
        Task<Lecturer> GetLecturerDetailAsync(string lecturerId);
        Task<bool> DeactivateLecturerAsync(string lecturerId);
    }
}
