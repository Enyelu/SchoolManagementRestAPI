using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class NonAcademicStaff
    {
        [Key]
        public string AppUserId { get; set; }
        public Department Department { get; set; }
        public Faculty Faculty { get; set; }
        public NonAcademicStaffPosition Position { get; set; }
        public AppUser AppUser { get; set; }    
    }
}