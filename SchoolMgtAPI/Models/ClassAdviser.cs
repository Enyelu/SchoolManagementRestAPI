using System;
using System.Collections.Generic;

namespace Models
{
    public class ClassAdviser
    {
        public string LecturerId { get; set; }
        public int Id {  get; set; }    
        public string Class { get; set; }  
        public DateTime DateTime { get; set; }
        public Lecturer Lecturer { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
