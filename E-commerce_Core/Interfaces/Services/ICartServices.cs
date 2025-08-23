using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.CartDtos;

namespace E_commerce_Core.Interfaces.Services
{
    public interface ICartServices
    {
        // add
        Task<ApiResponse<CartResponesDto>> AddtoCartAsync(AddtocartDto addToCartDto);
        // update

        Task <ApiResponse<CartResponesDto>>UpdateCartItemAsync(UpdateCartitemDto updataCartitemDto);
        // delete
        Task<ApiResponse<CartResponesDto>> RemoveCartItemDto(RemoveCartItemDto removeCartItemDto);

        //get
        Task<ApiResponse<CartResponesDto>> GetCartAsync(string userName);
        // helper
        Task <ApiResponse<CartResponesDto>> ProjectCartForUserAsync(string userName);
     

       


    }
}
