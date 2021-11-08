using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Student 
    {
        [Key]
        public string AppUserId { get; set; }
        public string RegistrationNumber { get; set; }
        public int Class { get; set; } 
        public int Level { get; set; }
        public AppUser AppUser { get; set; }
        public ClassAdviser ClassAdviser { get; set; }
        public Department Department { get; set; }
        public Faculty Faculty { get; set; }
        public ICollection<Course> Courses { get; set; }    
    }
}