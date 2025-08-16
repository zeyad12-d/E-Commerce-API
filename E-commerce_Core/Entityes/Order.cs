using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Entityes
{
    public class Order
    {
        public int BillingAddressId;

        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; } // Added this property to fix the error  
        public User User { get; set; }
        public int? ShoppingAddressId { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string paymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public Payment Payment { get; set; }
    }
}
