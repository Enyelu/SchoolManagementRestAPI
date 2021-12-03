using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Utilities.PaymentTokenGenerator;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private PayStackApi PayStack { get; set; }
        private readonly SchoolDbContext _context;
        private string token;
        public PaymentController(IConfiguration configuration, SchoolDbContext context, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            token = _configuration["Paystack:PaystackSK"];
            PayStack = new PayStackApi(token);
            _userManager = userManager;
        }

        [HttpPost("Payment")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> PaymentAsync([FromBody] PaymentModel paymentModel)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if(user != null)
                {
                    TransactionInitializeRequest request = new TransactionInitializeRequest
                    {
                        AmountInKobo = paymentModel.Amount * 100,
                        Currency = "NGN",
                        Email = paymentModel.Email,
                        CallbackUrl = $"{_configuration["AppUrl"]}/api/Payment/verify/",
                        Reference = PaymentToken.GenerateToken()
                    };

                    TransactionInitializeResponse response = PayStack.Transactions.Initialize(request);

                    if (response.Status)
                    {
                        PaymentRecord payment = new PaymentRecord()
                        {
                            Id = Guid.NewGuid().ToString(),
                            StudentId = user.Id,
                            StudentRegistrationNumber = user.Student.RegistrationNumber,
                            StudentFirstName = user.FirstName,
                            StudentLastName = user.LastName,
                            StudentEmail = user.Email,
                            StudentDepartment = user.Student.Department.Name,
                            StudentLevel = user.Student.Level,
                            Avatar = user.Avatar,
                            PaymentType = paymentModel.PaymentType,
                            Amount = request.AmountInKobo,
                            TransactionReference = request.Reference,
                            DateCreated = DateTime.Now.ToString()
                        };
                        await _context.PaymentRecords.AddAsync(payment);
                        await _context.SaveChangesAsync();
                        return Redirect(response.Data.AuthorizationUrl);
                    }
                    return BadRequest("Transaction failed to initialize");
                }
                return BadRequest("User does not exist");
            }
            return BadRequest("User does not exist");
        }

        [HttpGet("Verify")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> VerifyAsync(string reference)
        {
            var response = PayStack.Transactions.Verify(reference);

            if (response.Data.Status.ToLower().Trim() == "success")
            {
                var PaymentRecord = await _context.PaymentRecords.FirstOrDefaultAsync(x => x.TransactionReference == reference);
                if (PaymentRecord != null)
                {
                    PaymentRecord.IsApproved = true;
                    _context.PaymentRecords.Update(PaymentRecord);
                    await _context.SaveChangesAsync();
                    return Ok("Transaction successful");
                }
                return BadRequest("Record not found");
            }
            return BadRequest("Transaction was unsuccessful");
        }

        [HttpGet("View")]
        [Authorize(Roles = "Student")]
        public async Task<IEnumerable<PaymentRecord>> ViewPaymentsAsync()
        {
            return await _context.PaymentRecords.ToListAsync();
        }
    }
}