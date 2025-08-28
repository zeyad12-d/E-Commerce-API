using E_commerce_Core.DTO.ReviewDtos;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService review)
        {
            _reviewService = review;
        }

        
        [HttpPost("CreateReview")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateReview(CreateReviewDto dto)
        {
            var respones = await _reviewService.CreateReviewsAsync(dto);
            return StatusCode(respones.StatusCode, respones);
        }

       
        [HttpGet("GetProductReviews/{ProductId}")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> GetProductReviews(int ProductId)
        {
            var respones = await _reviewService.GetProductReviewsByID(ProductId);
            return StatusCode(respones.StatusCode, respones);
        }

        
        [HttpPut("UpdateReview")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdataReviewDto updataReview)
        {
            var respones = await _reviewService.UpdateReviewsAsync(updataReview);
            return StatusCode(respones.StatusCode, respones);
        }

        
        [HttpDelete("DeleteReview")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> DeleteReview([FromBody] DeleteReviewDto deleteReviewDto)
        {
            var respones = await _reviewService.DeleteReviewsAsync(deleteReviewDto);
            return StatusCode(respones.StatusCode, respones);
        }

        [HttpGet("GetReview/{ReviewId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReviewByID(int ReviewId)
        {
            var respones = await _reviewService.GetReviewByID(ReviewId);
            return StatusCode(respones.StatusCode, respones);
        }
    }
}
