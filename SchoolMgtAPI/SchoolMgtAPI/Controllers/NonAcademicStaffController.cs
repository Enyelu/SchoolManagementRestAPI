using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonAcademicStaffController : ControllerBase
    {
        private readonly INonAcademicStaffService _nonAcademicStaffService;
        public NonAcademicStaffController(INonAcademicStaffService nonAcademicStaffService)
        {
            _nonAcademicStaffService = nonAcademicStaffService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterNonAcademicStaffDto NonAcademicStaffDto)
        {
            var response = await _nonAcademicStaffService.RegisterNonAcademicStaff(NonAcademicStaffDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("AllNonAcademicStaff")]
        public async Task<IActionResult> ReadAllNonAcademicStaff()
        {
            var response = await _nonAcademicStaffService.ReadAllNonAcademicStaffAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> ReadNonAcademicStaff(EmailRequestDto email)
        {
            var response = await _nonAcademicStaffService.ReadNonAcademicStaffAsync(email);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateNonAcademicStaff(EmailRequestDto email)
        {
            var response = await _nonAcademicStaffService.DeactivateNonAcademicStaffAsync(email);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateNonAcademicStaff(NonAcademicStaffUpdateDto staff, string staffEmail)
        {
            var response = await _nonAcademicStaffService.UpdateNonAcademicStaffAsync(staff, staffEmail);
            return StatusCode(response.StatusCode, response);
        }
    }
}