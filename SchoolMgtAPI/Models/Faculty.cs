using System.Collections.Generic;

namespace Models
{
    public class Faculty
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Lecturer> Lecturer { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<NonAcademicStaff> NonAcademicStaff { get; set;}
    }
}
