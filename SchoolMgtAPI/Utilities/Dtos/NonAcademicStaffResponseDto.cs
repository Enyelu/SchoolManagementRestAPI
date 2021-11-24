using System;

namespace Utilities.Dtos
{
    public class NonAcademicStaffResponseDto
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }   
        public string Position  { get; set; }
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
