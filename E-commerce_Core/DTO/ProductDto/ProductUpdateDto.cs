using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerc_Servers.Services.DTO.ProductDto
{
    public class ProductUpdateDto
    {

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int CategoryId
        {
            get; set;
        }
        [Required(ErrorMessage = "At least one image URL is required")]
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
