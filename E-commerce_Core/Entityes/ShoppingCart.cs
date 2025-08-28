using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Entityes
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; } 

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public string userId { get; set; }

        public User User { get; set; }

        public bool ischeckedout { get; set; } = false;

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();


    }
}
