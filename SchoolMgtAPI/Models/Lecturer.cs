using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Lecturer  
    {
        [Key]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ClassAdviser ClassAdviser { get; set; }
        public Department Department { get; set; }
        public Faculty Faculty { get; set; }
        public ICollection<Course> Courses { get; set; }   
    }
}