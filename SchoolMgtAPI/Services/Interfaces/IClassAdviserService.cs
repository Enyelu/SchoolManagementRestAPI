using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IClassAdviserService
    {
        Task<Response<ClassAdviserResponseDto>> ReadClassAdviserAsync(ReadClassAdviserDto requestDto);
        Task<Response<string>> AssignClassAdviserAsync(AssignClassAdviserDto requestDto);
        Task<Response<string>> DeactivateClassAdviserAsync(ReadClassAdviserDto requestDto);
    }
}
