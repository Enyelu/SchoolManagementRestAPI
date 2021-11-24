using Models;
using System.Collections.Generic;

namespace Utilities.Dtos
{
    public class ClassAdviserResponseDto
    {
        public int Level { get; set; }
        public bool IsCourseAdviser { get; set; }
        public string DateTime { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public string BirthDate { get; set; }
        public string DateModified { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string Faculty { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
