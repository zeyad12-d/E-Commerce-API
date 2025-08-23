using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.PaymentDtos;

namespace E_commerce_Core.Interfaces.Services
{
    public interface IPaymentServices
    {
        Task<ApiResponse<PaymentResponseDTO>> ProcessPaymentAsync(PaymentRequestDTO paymentRequestDTO);

        Task<ApiResponse<PaymentResponseDTO>> GetPaymentsByIdAsync(int PaymentId);

        Task<ApiResponse<PaymentResponseDTO>> GetPaymentByOrderId(int OrderId);

        Task<ApiResponse<ConfirmationResponseDTO>> UpdataPaymentStautsAsync(PaymentStatusUpdateDTO paymentStatusUpdateDTO);

        Task<ApiResponse<ConfirmationResponseDTO>> CompleteCODPaymentAsync(CODPaymentUpdateDTO cODPaymentUpdateDTO);

        Task SendOrdeComfermationEmail(int orderID);

    }





}
