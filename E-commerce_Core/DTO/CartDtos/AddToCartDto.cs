using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.CartDtos
{
    public class AddtocartDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required."),Range(1,100,ErrorMessage ="must be between 1 to 100")]
        public int Quantity { get; set; }
    }
}
