using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.CategoryDTOs
{
    public class CategoryDetailsDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ParentCategoryDto ParentCategory { get; set; }
        public List<ChildCategoryDto> Children { get; set; }
        public List<ProductResponseDto> Products { get; set; }
    }

    public class ParentCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class ChildCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public  double Rating { get; set; }
    }

}
