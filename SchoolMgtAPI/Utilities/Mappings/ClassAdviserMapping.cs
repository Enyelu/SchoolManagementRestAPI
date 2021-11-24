using Models;
using System;

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
