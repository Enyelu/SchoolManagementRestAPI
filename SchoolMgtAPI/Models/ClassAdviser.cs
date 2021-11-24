using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ClassAdviser
    {
        [Key]
        public string LecturerId { get; set; }
        public int Level { get; set; }  
        public bool IsCourseAdviser { get; set; }
        public string DateTime { get; set; } 
        public Lecturer Lecturer { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
