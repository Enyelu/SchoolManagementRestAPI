using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Address
    {
        [Key]
        public string Id { get; set; } 
        public string StreetNumber { get; set; }    
        public string City { get; set; } 
        public string State { get; set; }   
        public string Country { get; set; } 
    }
}
