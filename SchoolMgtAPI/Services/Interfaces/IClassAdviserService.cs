using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IClassAdviserService
    {
        Task<Response<ClassAdviserResponseDto>> ReadClassAdviserAsync(int level, string department);
        Task<Response<string>> AssignClassAdviserAsync(string lecturerEmail, int level);
        Task<Response<string>> DeactivateClassAdviserAsync(int level, string department);
    }
}
