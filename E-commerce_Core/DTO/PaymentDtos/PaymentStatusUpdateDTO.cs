using E_commerce_Core.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.PaymentDtos
{
    public class PaymentStatusUpdateDTO
    {
        public int PaymentId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus Status { get; set; }

     
        public string? TransactionId { get; set; }
    }
}
