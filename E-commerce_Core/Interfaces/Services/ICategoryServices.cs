using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Interfaces.Services
{
    public interface ICategoryServices
    {
        // create
        Task<ApiResponse<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto);
        //read
        Task<ApiResponse<IEnumerable<CagtegoryResponesDto>>> GetAllCategoriesAsync();

        Task<ApiResponse<CagtegoryResponesDto>> GetCategoryByIdAsync(int id);
        Task<ApiResponse<CategoryDetailsDto>> GetCategoryDetailsByIdAsync(int id);

        //update
        Task<ApiResponse<bool>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);

        // delete
        Task<ApiResponse<bool>> DeleteCategoryAsync(int id);
        Task<ApiResponse<bool>> DeleteCategoryCascadeAsync(int id);

        

    }
}
