using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class NonAcademicStaff
    {
        [Key]
        public string AppUserId { get; set; }
        public NonAcademicStaffPosition DutyPost { get; set; }
        public AppUser AppUser { get; set; }    
    }
}
