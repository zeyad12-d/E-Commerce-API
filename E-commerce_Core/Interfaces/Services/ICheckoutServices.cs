using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.CartDtos;
using E_commerce_Core.DTO.OrderDtos;

namespace E_commerce_Core.Interfaces.Services
{
    public interface ICheckoutServices
    {
        Task<ApiResponse<OrderResponseDto>> CheckoutAsync(CheckoutRequestDTO checkoutRequest);
    }
}
