﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("Confirm-Email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]ConfirmEmailDto confirm)
        {
            var response = await _authService.ConfirmEmailAsync(confirm.Email, confirm.Token);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var response = await _authService.Login(loginDto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Forgot-Password")]
        public async Task<IActionResult> ForgotPassword( [FromQuery]EmailRequestDto forgortPassword)
        {
            var response = await _authService.ForgotPassword(forgortPassword);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromQuery]ResetPasswordModel resetPassword)
        {
            var response = await _authService.ResetPassword(resetPassword);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken( [FromBody] RefreshTokenRequestDto requestDto)
        {
            var response = await _authService.RefreshToken(requestDto);
            return StatusCode(response.StatusCode, response);
        }


    }
}
