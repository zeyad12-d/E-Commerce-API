using E_commerce_Core.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.CartDtos
{
    public class CheckoutRequestDTO
    {
        public string UserName { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int BillingAddressId { get; set; }
        public int ShippingAddressId { get; set; }

    }
}
