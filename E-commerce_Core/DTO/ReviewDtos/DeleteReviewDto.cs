using System.ComponentModel.DataAnnotations;

namespace E_commerce_Core.DTO.ReviewDtos
{
    public class DeleteReviewDto
    {
        [Required(ErrorMessage = "Review ID is required.")]
        public int  ReviewId { get; set; }
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; } = string.Empty;
    }
}
