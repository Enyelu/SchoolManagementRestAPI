namespace Utilities.Dtos
{
    public class StudentResponseDto
    {
        public string RegistrationNumber { get; set; }
        public int Class { get; set; } 
        public int Level { get; set; }
        public string FullName { get; set; }
        public string ClassAdviserName { get; set; }
        public string ClassAdviserEmail { get; set; }
        public string DepartmentName { get; set; }
        public string FacultyName { get; set; }
    }
}