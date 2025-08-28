using E_commerce_Core.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.OrderDtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string ShippingAddress { get; set; }

        public string BillingAddress { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus OrderStatus { get; set; } // e.g., Pending, Shipped, Delivered, Cancelled

        public decimal price { get; set; }  
        public decimal TotalAmount { get; set; }
        public List<OrderItemResponseDto> OrderItems { get; set; } = new List<OrderItemResponseDto>();

    }
}
