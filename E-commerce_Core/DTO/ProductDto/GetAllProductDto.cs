using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerc_Servers.Services.DTO.ProductDto
{
    public class GetAllProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public int CategoryId { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
