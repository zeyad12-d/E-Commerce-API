using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.CategoryDTOs
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [MaxLength(300), MinLength(10, ErrorMessage = "Description must be between 10 and 300 characters.")]
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }

    }
}
