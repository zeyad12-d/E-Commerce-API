using E_commerce_Core.DTO.OrderDtos;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

     
        [HttpPost("CreateOrder")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            var response = await _orderServices.CreateOrderAsync(orderDto);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return BadRequest(response);
        }

     
        [HttpGet("GetOrderById/{orderId}")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var response = await _orderServices.GetOrderByIdAsync(orderId);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return NotFound(response);
        }

        
        [HttpGet("GetAllOrders")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> GetAllOrders(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _orderServices.GetAllOrdersAsync(pageNumber, pageSize);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("CaltotalAmount/{orderId}")]
        [Authorize(Roles = "Admin,Vendor,Customer")]
        public async Task<IActionResult> CalTotalAmount(int orderId)
        {
            var response = await _orderServices.CalculateTotalAmountAsync(orderId);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return BadRequest(response);
        }

     
        [HttpGet("GetOrdersByStatus")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> GetOrdersByStatus(OrderStatus status, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _orderServices.GetOrdersByStatusAsync(status, pageNumber, pageSize);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return BadRequest(response);
        }

        
        [HttpDelete("DeleteOrder/{orderId}")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var response = await _orderServices.DeleteOrderAsync(orderId);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return NotFound(response);
        }

  
        [HttpPut("UpdateOrderStatus/{orderId}")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderStatus status)
        {
            var response = await _orderServices.UpdateOrderStatusAsync(orderId, status);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return BadRequest(response);
        }

       
        [HttpGet("GetOrdersByUserName/{userName}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetOrdersByUserName(string userName)
        {
            var response = await _orderServices.GetOrdersByUserNameAsync(userName);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return NotFound(response);
        }

        [HttpPost("CancelOrder/{orderId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderServices.CancelOrder(orderId);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return Ok(response);

            return NotFound(response);
        }
    }
}
