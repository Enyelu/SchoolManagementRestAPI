using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos
{
    public class ReadDepartmentDto
    {
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public bool IsActive { get; set; }
        public string Faculty { get; set; }
        public ICollection<Lecturer> Lecturer { get; set; }
        public ICollection<NonAcademicStaff> NonAcademicStaff { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
