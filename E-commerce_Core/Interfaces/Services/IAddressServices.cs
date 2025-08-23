using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.AddresDtos;

namespace E_commerce_Core.Interfaces.Services
{
    public interface IAddressServices
    {

        Task<ApiResponse<AddressResponseDto>> CreateAddressAsync(CreateAddressDto createAddressDto);

        Task<ApiResponse<AddressResponseDto>> UpdateAddressAsync(int id, UpdateAddressDto updateAddressDto);

        Task<ApiResponse<AddressResponseDto>> GetAddressByIdAsync(int id);

        Task<ApiResponse<IEnumerable<AddressResponseDto>>> GetAllAddressesAsync();

        Task<ApiResponse<bool>> DeleteAddressAsync(int id);

        Task<ApiResponse<IEnumerable<AddressResponseDto>>> GetAddressesByUserNameAsync(string userName);
    }
}
