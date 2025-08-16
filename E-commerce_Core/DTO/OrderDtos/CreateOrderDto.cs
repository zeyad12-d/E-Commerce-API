using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.OrderDtos
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "ShippingAddress is required.")]
       
        public int ShippingAddressId { get; set; }
        [Required(ErrorMessage = "BillingAddress is required.")]
        
        public int BillingAddressId { get; set; }

        [Required(ErrorMessage = "OrderItems are required.")]
        [MinLength(1, ErrorMessage = "At least one order item is required.")]

     
        public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();

    }
}
