using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public string Department { get; set; }
    }
}
