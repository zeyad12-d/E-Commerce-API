using AutoMapper;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.ReviewDtos;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerc_Servers.Services
{
    public class ReviewService:IReviewService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public ReviewService(UnitOfWork unitOfWork, IMapper mapper ,UserManager<User> user )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = user;
        }
        #region CreateReview

        public async Task<ApiResponse<ReviewResponesDto>> CreateReviewsAsync(CreateReviewDto createReviewDto)
        {
            try
            {
                if (createReviewDto == null)
                    return new ApiResponse<ReviewResponesDto>(400, "Invalid payload");


                var user = await _userManager.FindByNameAsync(createReviewDto.UserName);
                if (user == null)
                    return new ApiResponse<ReviewResponesDto>(404, "User not found");


                var product = await _unitOfWork.ProductRepo.Query()
                    .FirstOrDefaultAsync(p => p.ProductId == createReviewDto.ProductId);
                if (product == null)
                    return new ApiResponse<ReviewResponesDto>(404, "Product not found");


                var review = _mapper.Map<Review>(createReviewDto);
                review.UserId = user.Id;
                review.ProductId = product.ProductId;
                review.CreatedAt = DateTime.UtcNow;


                await _unitOfWork.ReviewRepo.AddAsync(review);
                await _unitOfWork.SaveChangesAsync();


                var createdReview = await _unitOfWork.ReviewRepo.Query()
                    .Include(r => r.Product)
                    .FirstOrDefaultAsync(r => r.ReviewId == review.ReviewId);

                if (createReviewDto.Rating < 1 || createReviewDto.Rating > 5)
                    return new ApiResponse<ReviewResponesDto>(400, "Rating must be between 1 and 5");

                var response = _mapper.Map<ReviewResponesDto>(createdReview);
                return new ApiResponse<ReviewResponesDto>(201, "Review created successfully", response);
            }
            catch (Exception ex) 
            {
                return new ApiResponse<ReviewResponesDto>(500, ex.Message);
            }
            }
        #endregion

        #region UpdateReviewsAsync
        public async Task<ApiResponse<ReviewResponesDto>> UpdateReviewsAsync(UpdataReviewDto updateReviewDto)
        {
            try
            {
                if (updateReviewDto == null)
                    return new ApiResponse<ReviewResponesDto>(400, "Invalid Payload");

                var review = await _unitOfWork.ReviewRepo.GetById(updateReviewDto.ReviewId);

                if (review == null)
                    return new ApiResponse<ReviewResponesDto>(404, $"Review with ID: {updateReviewDto.ReviewId} not found");

              
                review.Rating = updateReviewDto.Rating;
                review.Comment = updateReviewDto.Comment;
                 review.CreatedAt = DateTime.Now;

                _unitOfWork.ReviewRepo.Update(review);
                await _unitOfWork.SaveChangesAsync();

                
                var response = _mapper.Map<ReviewResponesDto>(review);

                return new ApiResponse<ReviewResponesDto>(200, "Review updated successfully", response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ReviewResponesDto>(500, ex.Message);
            }
        }
        #endregion

        #region DeleteReviews
        public async Task<ApiResponse<bool>> DeleteReviewsAsync(DeleteReviewDto deleteReviewDto)
        {
            try
            {
                if (deleteReviewDto == null) return new ApiResponse<bool>(400, "Invalid payload");

                var user = await _userManager.FindByNameAsync(deleteReviewDto.UserName);
                if (user == null) return new ApiResponse<bool>(404, "User Name Not Found");
                var review = await _unitOfWork.ReviewRepo.Query().FirstOrDefaultAsync(r => r.ReviewId == deleteReviewDto.ReviewId);
                if (review == null) return new ApiResponse<bool>(404, $"Review With Id:{deleteReviewDto.ReviewId} Not Found");
                if (review.UserId != user.Id)
                    return new ApiResponse<bool>(403, "You are not allowed to delete this review");

                await _unitOfWork.ReviewRepo.Delete(deleteReviewDto.ReviewId);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>(200, "Review Deleted Successfully",true);
            }
            catch (Exception ex) { return new ApiResponse<bool>(500, ex.Message); }
        }
        #endregion


        #region GetProductReviewsByID
        public async Task<ApiResponse<ProductReviewDtos>> GetProductReviewsByID(int productId)
        {
            try
            {
                // 1- تأكد أن المنتج موجود
                var product = await _unitOfWork.ProductRepo.Query()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                    return new ApiResponse<ProductReviewDtos>(404, "Product not found.");

                // 2- هات الريفيوهات مع الـ User (اللي هو AspNetUsers)
                var reviews = await _unitOfWork.ReviewRepo.Query()
                    .Where(r => r.ProductId == productId)
                    .Include(r => r.User)
                    .AsNoTracking()
                    .ToListAsync();

                double averageRating = 0;
                List<UserReviewDto> userReviews = new List<UserReviewDto>();

                if (reviews.Any())
                {
                    averageRating = reviews.Average(r => r.Rating);

                    userReviews = reviews.Select(r => new UserReviewDto
                    {
                        Id = r.ReviewId,
                        UserName = r.User.UserName,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        CreatedAt = r.CreatedAt,
                      
                    }).ToList();
                }

                // 3- Build response DTO
                var productReviewsResponse = new ProductReviewDtos
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    reviewCount = reviews.Count(),
                    AverageRating = Math.Round(averageRating, 2),
                    Reviews = userReviews
                };

                return new ApiResponse<ProductReviewDtos>(200, "Success", productReviewsResponse);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductReviewDtos>(500, $"Unexpected error occurred: {ex.Message}");
            }
        }

        #endregion

        #region GetReviewByID
        public async Task<ApiResponse<ReviewResponesDto>> GetReviewByID(int id)
        {
            try
            {
                var review = await _unitOfWork.ReviewRepo.Query()
                    .AsNoTracking().
                    Include(p => p.Product).
                    Include(u => u.User).
                    FirstOrDefaultAsync(P => P.ReviewId == id);


                if (review == null) return new ApiResponse<ReviewResponesDto>(404, "Review Not Found");

               var respones=  _mapper.Map<ReviewResponesDto>(review);
                return new ApiResponse<ReviewResponesDto>(200, "Review data", respones);

            }
            catch (Exception ex)
            {
                return new ApiResponse<ReviewResponesDto>(500,ex.Message);
            }
        }
        #endregion

       

   
        
    }
}
