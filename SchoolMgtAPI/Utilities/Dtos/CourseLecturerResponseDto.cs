
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos
{
    public class CourseLecturerResponseDto
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public string BirthDate { get; set; }
        public DateTime DateCreated { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
