using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappings
{
    public static class FacultyMap
    {
        public static Faculty FacultyMapping(string facultyName)
        {
            Faculty faculty = new Faculty()
            {
                Id = Guid.NewGuid().ToString(),
                Name = facultyName.Trim(),
                Date = DateTime.Now.ToString(),
                IsActive = true,
            };
            return faculty;
        }
    }
}
