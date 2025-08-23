using E_commerce_Core.Entityes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.PaymentDtos
{
    public class PaymentRequestDTO
    {
        [Required]
        public int OrderId { get; set; }

        public string? UserName { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
