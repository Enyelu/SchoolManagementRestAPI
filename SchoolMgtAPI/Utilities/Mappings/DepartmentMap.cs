using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappings
{
    public static class DepartmentMap
    {
        public static Department DepartmentMapping(string departmentName)
        {
            return new Department()
            {
                Id = Guid.NewGuid().ToString(),
                Name = departmentName,
                DateCreated = DateTime.Now.ToString(),
                IsActive = true,
            };
        }
    }
}
