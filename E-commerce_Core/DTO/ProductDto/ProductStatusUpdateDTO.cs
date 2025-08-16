using System.ComponentModel.DataAnnotations;

namespace E_commerce_Core.DTO.ProductDto
{
    public class ProductStatusUpdateDTO
    {

        [Required(ErrorMessage ="ProductId Is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "IsAvailable is required.")]
        public bool IsAvailable { get; set; }
    }
}
