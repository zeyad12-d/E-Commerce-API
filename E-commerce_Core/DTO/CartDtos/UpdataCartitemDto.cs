using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.CartDtos
{
    public class UpdataCartitemDto
    {
        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "CartItemId Is Required")]
        public int CartItemId { get; set; }

        
        public int Quantity { get; set; }


    }
}
