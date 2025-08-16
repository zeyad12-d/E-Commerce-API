using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Entityes
{
    public class OrderItem
    {
        public int orderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } 
    }
}
