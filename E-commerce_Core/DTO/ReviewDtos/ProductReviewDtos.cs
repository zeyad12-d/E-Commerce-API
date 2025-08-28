using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.ReviewDtos
{
    public class ProductReviewDtos
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int reviewCount { get; set; }
        public List<UserReviewDto> Reviews { get; set; }=new List<UserReviewDto>();
    }
}
