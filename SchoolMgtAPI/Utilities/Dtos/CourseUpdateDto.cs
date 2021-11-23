using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos
{
    public class CourseUpdateDto
    {
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public int CourseUnit { get; set; }
        public string NewCourseCode { get; set; }
    }
}
