using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface ILecturerService
    {
        Task<Response<string>> RegisterLecturerAsync(RegisterLecturerDto lecturerDto);
        Task<Response<LecturerResponseDto>> ReadLecturerDetailAsync(string lecturerEmail);
        Task<Response<string>> DeactivateLecturerAsync(string lecturerEmail);
        Task<Response<string>> UpdateLecturerAsync(LecturerDto lecturerDto, string lecturerEmail);
        Task<Response<string>> AssignCourseToLecturerAsync(string lecturerEmail, string courseName, string courseCode);
    }
}
