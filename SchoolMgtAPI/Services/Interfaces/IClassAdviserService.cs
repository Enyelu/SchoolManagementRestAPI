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
        Task<Response<ClassAdviserResponseDto>> ReadClassAdviser(int level, string department);
        Task<Response<string>> AssignClassAdviser(string lecturerEmail, int level);
    }
}
