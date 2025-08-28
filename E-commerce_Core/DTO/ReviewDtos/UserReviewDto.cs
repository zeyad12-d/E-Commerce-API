using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.ReviewDtos
{
    public  class UserReviewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }= DateTime.Now;
    }
}
