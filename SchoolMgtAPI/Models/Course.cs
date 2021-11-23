using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Course
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public int CourseUnit { get; set; }
        public string DateCreated { get; set; } = DateTime.UtcNow.ToString();
        public string DateModified { get; set; }
        public bool IsActive { get; set; }
        public Faculty Faculty { get; set; }
        public Department Department { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}