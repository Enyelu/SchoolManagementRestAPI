namespace Utilities.Dtos
{
    public class FacultyDepartmentsResponseDto
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public bool IsActive { get; set; }
        public int NoOfLecturer { get; set; }
        public int NoOfNonAcademicStaff { get; set; }
        public int NoOfCourses { get; set; }
    }
}
