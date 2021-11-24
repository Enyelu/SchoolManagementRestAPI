using Models;
using System;

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
                DateCreated = DateTime.UtcNow.ToString(),
                IsActive = true,
            };
        }
    }
}
