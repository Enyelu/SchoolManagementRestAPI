using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonAcademicStaffServiceController : ControllerBase
    {
        private readonly INonAcademicStaffService _nonAcademicStaffService;
        public NonAcademicStaffServiceController(INonAcademicStaffService nonAcademicStaffService)
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

        [HttpGet("NonAcademicStaff")]
        public async Task<IActionResult> ReadNonAcademicStaff(string staffEmail)
        {
            var response = await _nonAcademicStaffService.ReadNonAcademicStaffAsync(staffEmail);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateNonAcademicStaff(string staffEmail)
        {
            var response = await _nonAcademicStaffService.DeactivateNonAcademicStaffAsync(staffEmail);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateNonAcademicStaff(NonAcademicStaffDto staff, string staffEmail)
        {
            var response = await _nonAcademicStaffService.UpdateNonAcademicStaffAsync(staff, staffEmail);
            return StatusCode(response.StatusCode, response);
        }
    }
}