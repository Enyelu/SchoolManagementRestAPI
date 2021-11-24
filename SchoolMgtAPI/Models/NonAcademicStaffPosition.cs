using System;
using System.Collections.Generic;

namespace Models
{
    public class NonAcademicStaffPosition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ICollection<NonAcademicStaff> NonAcademicStaff {  get; set; }
    }
}
