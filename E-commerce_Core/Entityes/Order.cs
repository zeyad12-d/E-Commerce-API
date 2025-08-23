using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Entityes
{
    public class Order
    {
        public int BillingAddressId { get; set; }       

        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; } 
        public User User { get; set; }
        public int? ShoppingAddressId { get; set; }
        public Address ShippingAddress { get; set; }

        public Address BillingAddress{ get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string paymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public Payment Payment { get; set; }
    }
}
