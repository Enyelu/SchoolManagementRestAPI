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
        public DateTime Date { get; set; }
        public int NoOfStaff { get; set; }  
        public bool IsActive { get; set; }  
        public ICollection<Course> Courses { get; set; }
    }
}
