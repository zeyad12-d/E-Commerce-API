using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Interfaces.Services
{
    public interface IReviewService
    {
        //create
       Task<ApiResponse<ReviewResponesDto>> CreateReviewsAsync(CreateReviewDto createReviewDto);
        
        Task<ApiResponse<ReviewResponesDto>>UpdateReviewsAsync(UpdataReviewDto updateReviewDto);

        Task<ApiResponse<bool>> DeleteReviewsAsync(DeleteReviewDto deleteReviewDto);

        Task<ApiResponse<ReviewResponesDto>>GetReviewByID(int id);

        Task<ApiResponse<ProductReviewDtos>> GetProductReviewsByID(int ProductId);

        


    }
}
