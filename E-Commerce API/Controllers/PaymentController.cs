using E_commerce_Core.DTO.PaymentDtos;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;

        public PaymentController(IPaymentServices payment)
        {
            _paymentServices = payment;
        }

        [HttpPost("Pay")]
        public async Task<IActionResult> PaymentProcess([FromBody] PaymentRequestDTO paymentRequestDTO)
        {
            var response = await _paymentServices.ProcessPaymentAsync(paymentRequestDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] PaymentStatusUpdateDTO updatePaymentStatusDTO)
        {
            var response = await _paymentServices.UpdataPaymentStautsAsync(updatePaymentStatusDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("CompleteCOD")]
        public async Task<IActionResult> CompleteCOD([FromBody] CODPaymentUpdateDTO codDto)
        {
            var response = await _paymentServices.CompleteCODPaymentAsync(codDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("ByOrder/{orderId}")]
        public async Task<IActionResult> GetPaymentByOrderId([FromRoute] int orderId)
        {
            var response = await _paymentServices.GetPaymentByOrderId(orderId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("ById/{paymentId}")]
        public async Task<IActionResult> GetPaymentById([FromRoute] int paymentId)
        {
            var response = await _paymentServices.GetPaymentsByIdAsync(paymentId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("SendConfirmOrderEmail/{orderId}")]
        public async Task<IActionResult> SendConfirmationEmail([FromRoute] int orderId)
        {
             await  _paymentServices.SendOrdeComfermationEmail(orderId);

            return Ok(new { Message = "Confirmation email sent successfully." });
        }
    }
}
