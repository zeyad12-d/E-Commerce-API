using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.CartDtos
{
    public class CartResponesDto
    {
        public int ShoppingCartId { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public string UserName { get; set; }
        public bool ischeckedout { get; set; } = false;
        public ICollection<CartitemResponesDto>? Items { get; set; } = new List<CartitemResponesDto>();
    }
}
