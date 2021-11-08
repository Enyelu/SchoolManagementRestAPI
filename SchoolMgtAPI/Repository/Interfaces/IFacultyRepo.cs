using Models;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IFacultyRepo : IGenericRepository<Faculty>
    {
        Task<Faculty> GetFacultyAsync(string facultyName);
    }
}
