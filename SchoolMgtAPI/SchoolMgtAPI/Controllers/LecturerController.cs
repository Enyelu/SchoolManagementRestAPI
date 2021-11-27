﻿using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterLecturer(RegisterLecturerDto lecturerDto)
        {
            var respose = await _lecturerService.RegisterLecturerAsync(lecturerDto);
            return StatusCode(respose.StatusCode, respose);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> ReadLecturerDetail(EmailRequestDto email)
        {
            var respose = await _lecturerService.ReadLecturerDetailAsync(email);
            return StatusCode(respose.StatusCode, respose);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateLecturer(EmailRequestDto lecturerEmail)
        {
            var respose = await _lecturerService.DeactivateLecturerAsync(lecturerEmail);
            return StatusCode(respose.StatusCode, respose);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLecturerAsync(LecturerUpdateDto lecturerDto, string lecturerEmail)
        {
            var respose = await _lecturerService.UpdateLecturerAsync(lecturerDto, lecturerEmail);
            return StatusCode(respose.StatusCode, respose);
        }

        [HttpGet("AssignCourses")]
        public async Task<IActionResult> AssignCourseToLecturer(AssignCourseDto CourseDto)
        {
            var response = await _lecturerService.AssignCourseToLecturerAsync(CourseDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}