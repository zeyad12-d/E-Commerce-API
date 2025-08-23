using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public TestEmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("send")]
        public async Task<IActionResult> SendTestEmail()
        {
            await _emailService.SendEmailAsync(
                "zeadyasser054@gmail.com",
                "Test Email",
                "<h2>Hello from E-Commerce App 👋</h2>",
                true
            );

            return Ok("Email Sent!");
        }
    }
}