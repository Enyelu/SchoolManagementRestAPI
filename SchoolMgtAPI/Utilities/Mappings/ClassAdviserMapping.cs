using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappings
{
    public static class ClassAdviserMapping
    {
        public static ClassAdviser MapClassAdviser(int level)
        {
            return new ClassAdviser()
            {
                    Level = level,
                    IsCourseAdviser = true,
                    DateTime = DateTime.UtcNow.ToString(),
             };
        }
    }
}
