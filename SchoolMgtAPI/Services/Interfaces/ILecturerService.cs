using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface ILecturerService
    {
        Task<Response<string>> RegisterLecturerAsync(RegisterLecturerDto lecturerDto);
        Task<Response<LecturerResponseDto>> ReadLecturerDetailAsync(EmailRequestDto lecturerEmail);
        Task<Response<string>> UpdateLecturerAsync(LecturerUpdateDto lecturerDto, string lecturerEmail);
        Task<Response<string>> AssignCourseToLecturerAsync(AssignCourseDto CourseDto);
    }
}
