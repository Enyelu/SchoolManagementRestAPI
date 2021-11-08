using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
     public interface IStudentService
     {
        Task<Response<ReadStudentResponseDto>> ReadStudentAsync(string resgitrationNumber);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInALevelAsync(int studentsLevel);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInADepartmentInALevelAsync(int studentsLevel, string department);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInAFacultyInALevelAsync(int studentsLevel, string faculty);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInDepartmentAsync(string department);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInFacultyAsync(string faculty);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentAsync();
        Task<Response<string>> DeactivateStudentAsync(string registrationNumber);
        Task<Response<string>> CheckStudentIsActiveAsync(string registrationNumber);
        Task<Response<IEnumerable<Course>>> RegisterCoursesAsync(string studentId, ICollection<string> courses);
        Task<Response<IEnumerable<string>>> RemoveCoursesAsync(string studentId, ICollection<string> courses);
        Task<Response<IEnumerable<Course>>> ReadRegisteredCoursesAsync(string studentId);
     }
}

