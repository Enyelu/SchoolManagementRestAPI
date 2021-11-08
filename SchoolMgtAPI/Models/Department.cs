using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Department
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public bool IsActive { get; set; } 
        public Faculty Faculty { get; set; }
        public ICollection<Lecturer> Lecturer {  get; set; } 
        public ICollection<NonAcademicStaff> NonAcademicStaff {  get; set;}
        public ICollection<Course> Courses { get; set; }
    }
}
