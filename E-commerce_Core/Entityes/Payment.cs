using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Entityes
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        
        public string PaymentMethod { get; set; } // e.g., CreditCard, PayPal, CashOnDelivery

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string PaymentStatus { get; set; } // Success, Failed, Pending
        public string? TransactionId { get; set; }
    }
}
