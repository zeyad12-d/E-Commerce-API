using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Interfaces.Services
{
    public interface IOrderServices
    {
        Task<ApiResponse<OrderResponseDto>> CreateOrderAsync(CreateOrderDto orderDto);
        Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(int orderId);
        Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetAllOrdersAsync(int PageNumber,int PageSize);
        Task<ApiResponse<bool>> UpdateOrderStatusAsync(int orderId, string status);
        Task<ApiResponse<bool>> DeleteOrderAsync(int orderId);
        Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetOrdersByStatusAsync(string status);
        Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetOrdersByUserNameAsync(string userId);

        Task<ApiResponse<decimal>> CalculateTotalAmountAsync(int orderId);
        
      
    }
}
