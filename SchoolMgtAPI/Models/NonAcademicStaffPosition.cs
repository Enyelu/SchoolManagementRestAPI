using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
