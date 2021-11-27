﻿using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace SchoolMgtAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class ClassAdviserController : ControllerBase
    {
        private readonly IClassAdviserService _classAdviserService;

        public ClassAdviserController(IClassAdviserService classAdviserService)
        {
            _classAdviserService = classAdviserService;
        }

        [HttpGet("ClassAdviser")]
        public async Task<IActionResult> ReadClassAdviser(int level, string department)
        {
            var response = await _classAdviserService.ReadClassAdviserAsync(level, department);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("ClassAdviser")]
        public async Task<IActionResult> AssignClassAdviser(string lecturerEmail, int level)
        {
            var response = await _classAdviserService.AssignClassAdviserAsync(lecturerEmail, level);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("DeactivateClassAdviser")]
        public async Task<IActionResult> DeactivateClassAdviserAsync(int level, string department)
        {
            var response = await _classAdviserService.DeactivateClassAdviserAsync(level, department);
            return StatusCode(response.StatusCode, response);
        }
    }
}