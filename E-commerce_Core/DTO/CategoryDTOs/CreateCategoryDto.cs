using System.ComponentModel.DataAnnotations;

namespace E_commerce_Core.DTO.CategoryDTOs
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [MaxLength(300),MinLength(10 ,ErrorMessage = "Description must be between 10 and 300 characters.")]
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; } 
    }
}
