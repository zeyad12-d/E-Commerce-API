using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.ReviewDtos
{
    public class UpdataReviewDto
    {
        [Required(ErrorMessage ="Review ID Is Required")]
        public int  ReviewId { get; set; }
        [Required(ErrorMessage ="Product ID Is Required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string? Comment { get; set; }
    }
}
