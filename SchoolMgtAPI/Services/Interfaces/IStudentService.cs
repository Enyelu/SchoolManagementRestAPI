using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IStudentService
    {
        Task<Response<string>> RegisterStudentAsync(RegisterStudentDto student);
        Task<Response<ReadStudentResponseDto>> ReadStudentAsync(RegistrationNumberDto Number);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInALevelAsync(LevelDto studentsLevel);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInADepartmentInALevelAsync(ReadDepartmentsStudentDto requestDto);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInAFacultyInALevelAsync(ReadFacultyStudentsDto requestDto);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInDepartmentAsync(NameDto department);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInFacultyAsync(NameDto faculty);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentAsync();
        Task<Response<string>> DeactivateStudentAsync(RegistrationNumberDto registrationNumber);
        Task<Response<string>> CheckStudentIsActiveAsync(RegistrationNumberDto registrationNumber);
        Task<Response<IEnumerable<string>>> RegisterCoursesAsync(string studentId, ICollection<string> courses);
        Task<Response<IEnumerable<string>>> RemoveCoursesAsync(string studentId, ICollection<string> courses);
        Task<Response<IEnumerable<string>>> ReadRegisteredCoursesAsync(string studentId);
    }
}

