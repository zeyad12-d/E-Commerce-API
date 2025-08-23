using E_commerce_Core.Entityes;
using System.Text.Json.Serialization;

namespace E_commerce_Core.DTO.PaymentDtos
{
    public class PaymentResponseDTO
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }

        public string? TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus PaymentStatus { get; set; }
    }
}
