using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.CartDtos;

namespace E_commerce_Core.Interfaces.Services
{
    public interface ICartServices
    {
        Task <ApiResponse<CartResponesDto>> GetCartByUserNameAsync(string userName);
        Task<ApiResponse<AddToCartDto>>  AddToCart(AddToCartDto addToCartDto);

        Task<ApiResponse <bool>> RemoveFromCart(int productId, string userName);

        Task <ApiResponse <bool>>UpdateCart( string UserName , UpdataCartitemDto updataCartitemDto);


    }
}
