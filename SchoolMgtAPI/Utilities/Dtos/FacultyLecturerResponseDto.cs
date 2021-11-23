using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dtos
{
    public class FacultyLecturerResponseDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public string Department { get; set; }
        public string BirthDate { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }
    }
}