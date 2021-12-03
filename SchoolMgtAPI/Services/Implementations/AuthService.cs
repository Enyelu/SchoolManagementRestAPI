using Microsoft.AspNetCore.Identity;
using Models;
using Services.Interfaces;
using System;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;
using Utilities.Interface.TokenGeneration;

namespace Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordService _passwordService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IPasswordService passwordService, UserManager<AppUser> userManager, ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork)
        {
            _passwordService = passwordService;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<string>> ConfirmEmailAsync(string email, string token)
        {
            var respose = await _passwordService.ConfirmEmailAsync(email, token);

            if (respose.Succeeded)
            {
                return Response<string>.Success(null, "Email confirmation was successful");
            }
            return Response<string>.Fail("Email confirmation failed");
        }

        public async Task<Response<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            if (user != null)
            {
                var validateUserPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if(validateUserPassword )
                {
                    var userEmailIsConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                    if (userEmailIsConfirmed)
                    {
                        var token = await _tokenGenerator.GenerateTokenAsync(user);

                        if(token != null)
                        {
                            user.RefereshToken = _tokenGenerator.GenerateRefreshToken();
                            user.RefereshTokenExpiry = DateTime.Now.AddDays(10);
                            await _userManager.UpdateAsync(user);

                            var response = new LoginResponseDto()
                            {
                                Token = token,
                                RefreshToken = user.RefereshToken,
                                UserId = user.Id
                            };
                            return Response<LoginResponseDto>.Success(response, "Login Successful");
                        }
                        
                    }
                    return Response<LoginResponseDto>.Success(null, $"Email not confirmed. Please visit {loginDto.Password} to confirm your password");
                }
                return Response<LoginResponseDto>.Fail("Invalid login credentials");
            }
            return Response<LoginResponseDto>.Fail($"{loginDto.Email} is not a registered user");
        }

        public async Task<Response<string>> ForgotPassword(EmailRequestDto passwordRequest)
        {
            return await _passwordService.ForgotPasswordAsync(passwordRequest.Email);
        }

        public async Task<Response<string>> ResetPassword(ResetPasswordModel passwordRequest)
        {
            return await _passwordService.ResetPasswordAsync(passwordRequest);
        }

        public async Task<Response<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestDto tokenRequestDto)
        {
            var user = await _userManager.FindByIdAsync(tokenRequestDto.UserId);

            if (user != null)
            {
                if(user.RefereshToken == tokenRequestDto.RefreshToken)
                {
                    if(user.RefereshTokenExpiry > DateTime.Now)
                    {
                        var token = await _tokenGenerator.GenerateTokenAsync(user);
                        user.RefereshToken = _tokenGenerator.GenerateRefreshToken();
                        user.RefereshTokenExpiry = DateTime.Now.AddDays(10);
                        await _userManager.UpdateAsync(user);

                        var response = new RefreshTokenResponseDto()
                        {
                            NewRefreshToken = user.RefereshToken,
                            NewAccessToken = token
                        };
                        return Response<RefreshTokenResponseDto>.Success(response, "Transaction successful");
                    }
                    return Response<RefreshTokenResponseDto>.Fail("Refresh token still valid");
                }
                return Response<RefreshTokenResponseDto>.Fail("Transaction failed. Invalid refresh token");
            }
            return Response<RefreshTokenResponseDto>.Fail("Transaction failed. Unauthorized user");
        }
    }
}
