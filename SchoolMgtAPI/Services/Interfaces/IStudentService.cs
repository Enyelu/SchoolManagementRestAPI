using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IStudentService
    {
        Task<Response<Student>> ReadStudentAsync(string studentId);
        Task<Response<IEnumerable<Student>>> ReadAllStudentAsync();
        Task<Response<string>> DeactivateStudentAsync(string studentId);
        Task<Response<string>> CheckStudentIsActiveAsync(string studentId);
        Task<Response<IEnumerable<Course>>> RegisterCoursesAsync(string studentId, ICollection<Course> courses);
        Task<Response<IEnumerable<Course>>> RemoveCoursesAsync(string studentId, ICollection<Course> courses);
        Task<Response<IEnumerable<Course>>> ReadRegisteredCoursesAsync(string studentId);
    }
}
