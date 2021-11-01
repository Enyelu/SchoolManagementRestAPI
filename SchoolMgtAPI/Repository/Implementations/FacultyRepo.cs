using Data;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class FacultyRepo : IFacultyRepo
    {
        private readonly SchoolDbContext _context;
        public FacultyRepo(SchoolDbContext context)
        {
            _context = context;
        }
    }
}
