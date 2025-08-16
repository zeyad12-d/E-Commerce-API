using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Entityes
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; } // PK واضح

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();


    }
}
