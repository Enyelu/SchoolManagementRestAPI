using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class LecturerRepo : GenericRepository<Lecturer>, ILecturerRepo
    {
        private readonly SchoolDbContext _context;
        public LecturerRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Lecturer> GetLecturerDetailAsync(string lecturerEmail)
        {
           var lecturer =  await _context.Lecturers.Include(x => x.Courses)
                                                   .Include(x => x.AppUser)
                                                   .Include(x => x.Department)
                                                   .Include(x => x.Faculty)
                                                   .Include(x => x.AppUser.Address)
                                                   .FirstOrDefaultAsync(x => x.AppUser.Email == lecturerEmail);
           
            if(lecturer != null) { return lecturer; }
            
            return null;
        }
    }
}
