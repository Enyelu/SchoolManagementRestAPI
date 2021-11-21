using Data;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class ClassAdviserRepo : IClassAdviserRepo
    {
        private readonly SchoolDbContext _context;
        public ClassAdviserRepo(SchoolDbContext context)
        {
            _context = context;
        }

        //public async Task<ClassAdviser>

        //add class ad. to studen
        //add class adviser
        //delete class adviser from students
    }
}
