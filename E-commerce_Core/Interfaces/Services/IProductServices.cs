using E_commerc_Servers.Services.DTO.ProductDto;
using E_commerce_Core.ApiRespones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Interfaces.Services
{
    public interface IProductServices
    {


        // Create
        Task<ApiResponse<GetAllProductDto>> CreateProductAsync(ProductCreateDto productDto);

        // Read
        Task<ApiResponse<IEnumerable<GetAllProductDto>>> GetAllProductsAsync(int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<GetAllProductDto>> GetProductByIdAsync(int id);
        Task<ApiResponse<IEnumerable<GetAllProductDto>>> GetAllProductsByCategoryAsync(int categoryId, int pageNumber = 1, int pageSize = 10);

        // Search
        Task<ApiResponse<IEnumerable<GetAllProductDto>>> SearchProductsAsync(string searchTerm, int pageNumber = 1, int pageSize = 10);

        // Update
        Task<ApiResponse<bool>> UpdateProductAsync(int id, ProductUpdateDto productDto);
        Task<ApiResponse<bool>> UpdateProductStatusAsync(int id, bool isActive);

        // Delete
        Task<ApiResponse<bool>> DeleteProductAsync(int id);
    }
}
