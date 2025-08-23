using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.PaymentDtos;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace E_commerc_Servers.Services
{
    public class PaymentServices:IPaymentServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly EmailService _EmailService;
        public PaymentServices(EmailService emailService,UnitOfWork unitOfWork)
        {
           _EmailService = emailService;
            _unitOfWork = unitOfWork;
        }
        #region processPayment
        public async Task<ApiResponse<PaymentResponseDTO>> ProcessPaymentAsync(PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                var order = await _unitOfWork.OrderRepo.Query().
                    Include(u => u.Payment).FirstOrDefaultAsync(o=>o.OrderId == paymentRequestDTO.OrderId&&o.UserName==paymentRequestDTO.UserName);
                if (order ==null) return new ApiResponse<PaymentResponseDTO>(404,"Order Not Found");

                if (Math.Round(paymentRequestDTO.Amount, 2)!= Math.Round(order.TotalAmount, 2))
                    return new ApiResponse<PaymentResponseDTO>(400,"")
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaymentResponseDTO>(500, "An unexpected error occurred while processing the payment.");
            }
        }
            #endregion
        public Task<ApiResponse<ConfirmationResponseDTO>> CompleteCODPaymentAsync(CODPaymentUpdateDTO cODPaymentUpdateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PaymentResponseDTO>> GetPaymentByOrderId(int OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PaymentResponseDTO>> GetPaymentsByIdAsync(int PaymentId)
        {
            throw new NotImplementedException();
        }

        

        public Task SendOrdeComfermationEmail(int orderID)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ConfirmationResponseDTO>> UpdataPaymentStautsAsync(PaymentStatusUpdateDTO paymentStatusUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
