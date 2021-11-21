using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Address
    {
        [Key]
        public string Id { get; set; } =  Guid.NewGuid().ToString();
        public string StreetNumber { get; set; }    
        public string City { get; set; } 
        public string State { get; set; }   
        public string Country { get; set; } 
    }
}
