using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Course
    {
        [Key]
        public string Id { get; set; } 
        public string Name { get; set; }
        public string CourseCode { get; set;}
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public Department Department { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}
