using AutoMapper;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.PaymentDtos;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace E_commerc_Servers.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly EmailService _EmailService;
        private readonly IMapper _mapper;

        public PaymentServices(EmailService emailService, UnitOfWork unitOfWork, IMapper mapper)
        {
            _EmailService = emailService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region processPayment
        public async Task<ApiResponse<PaymentResponseDTO>> ProcessPaymentAsync(PaymentRequestDTO paymentRequest)
        {
            try
            {
                var order = await _unitOfWork.OrderRepo.Query()
                    .Include(u => u.Payment)
                    .FirstOrDefaultAsync(o => o.OrderId == paymentRequest.OrderId && o.UserName == paymentRequest.UserName);

                if (order == null)
                    return new ApiResponse<PaymentResponseDTO>(404, "Order Not Found");

                if (Math.Round(paymentRequest.Amount, 2) != Math.Round(order.TotalAmount, 2))
                    return new ApiResponse<PaymentResponseDTO>(400, "Payment Amount Is Not Match With Total order");

                Payment payment;

                if (order.Payment != null)
                {
                    if (order.Payment.paymentStatus == PaymentStatus.Failed && order.OrderStatus == OrderStatus.Pending)
                    {
                        payment = order.Payment;
                        payment.paymentMethod = paymentRequest.PaymentMethod;
                        payment.Amount = paymentRequest.Amount;
                        payment.PaymentDate = DateTime.UtcNow;
                        payment.paymentStatus = PaymentStatus.Pending;
                        payment.TransactionId = null;

                        _unitOfWork.PaymentRepo.Update(payment);
                    }
                    else
                    {
                        return new ApiResponse<PaymentResponseDTO>(400, "Order already has an associated payment.");
                    }
                }
                else
                {
                    payment = new Payment
                    {
                        OrderId = paymentRequest.OrderId,
                        paymentMethod = paymentRequest.PaymentMethod,
                        Amount = paymentRequest.Amount,
                        PaymentDate = DateTime.UtcNow,
                        paymentStatus = PaymentStatus.Pending,
                    };
                    await _unitOfWork.PaymentRepo.AddAsync(payment);
                }

                // 🔹 Simulate payment gateway if not COD
                if (!IsCashOnDelivery(paymentRequest.PaymentMethod))
                {
                    var simulatedStatus = await SimulatePaymentGateway();
                    payment.paymentStatus = simulatedStatus;

                    if (simulatedStatus == PaymentStatus.Completed)
                        payment.TransactionId = GenerateTransactionId();
                }
                else
                {
                    payment.paymentStatus = PaymentStatus.Pending;
                }

                
                UpdateOrderStatusBasedOnPayment(order, payment);

                await _unitOfWork.SaveChangesAsync();

               
                if (order.OrderStatus == OrderStatus.Processing)
                    await SendOrderConfirmationEmailAsync(paymentRequest.OrderId);

                var respones = _mapper.Map<Payment, PaymentResponseDTO>(payment);
                return new ApiResponse<PaymentResponseDTO>(200, "Success", respones);
            }
            catch
            {
                return new ApiResponse<PaymentResponseDTO>(500, "An unexpected error occurred while processing the payment.");
            }
        }
        #endregion

        #region GetPaymentByID
        public async Task<ApiResponse<PaymentResponseDTO>> GetPaymentsByIdAsync(int PaymentId)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepo.Query()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.PaymentId == PaymentId);

                if (payment == null)
                    return new ApiResponse<PaymentResponseDTO>(404, "Payment Not found");

                var respones = _mapper.Map<PaymentResponseDTO>(payment);

                return new ApiResponse<PaymentResponseDTO>(200, "success", respones);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaymentResponseDTO>(500, $"An unexpected error occurred while retrieving the payment. {ex.Message}");
            }
        }
        #endregion

        #region GetPaymentByOrderID
        public async Task<ApiResponse<PaymentResponseDTO>> GetPaymentByOrderId(int OrderId)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepo.Query()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.OrderId == OrderId);

                if (payment == null)
                    return new ApiResponse<PaymentResponseDTO>(404, "Payment Not Found");

                var respones = _mapper.Map<PaymentResponseDTO>(payment);
                return new ApiResponse<PaymentResponseDTO>(200, "success", respones);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaymentResponseDTO>(500, $"An unexpected error occurred while retrieving the payment. {ex.Message}");
            }
        }
        #endregion

        #region UpdatePaymentStatusAsync
        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdataPaymentStautsAsync(PaymentStatusUpdateDTO paymentStatusUpdateDTO)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepo.Query()
                    .Include(p => p.Order)
                    .FirstOrDefaultAsync(p => p.PaymentId == paymentStatusUpdateDTO.PaymentId);

                if (payment == null)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Payment not found");

                if (IsCashOnDelivery(payment.paymentMethod) && paymentStatusUpdateDTO.Status == PaymentStatus.Completed)
                    return new ApiResponse<ConfirmationResponseDTO>(409, "Use COD completion endpoint for Cash on Delivery payments.");

                _mapper.Map(paymentStatusUpdateDTO, payment);

                if (payment.paymentStatus == PaymentStatus.Completed && !IsCashOnDelivery(payment.paymentMethod))
                {
                    if (string.IsNullOrWhiteSpace(payment.TransactionId))
                        payment.TransactionId = GenerateTransactionId();
                }

                // 🔹 Update order status
                UpdateOrderStatusBasedOnPayment(payment.Order, payment);

                _unitOfWork.PaymentRepo.Update(payment);
                await _unitOfWork.SaveChangesAsync();

                if (payment.Order.OrderStatus == OrderStatus.Processing)
                    await SendOrderConfirmationEmailAsync(payment.Order.OrderId);

                return new ApiResponse<ConfirmationResponseDTO>(200, "Success",
                    new ConfirmationResponseDTO { Message = $"Payment with ID {payment.PaymentId} updated to '{payment.paymentStatus}'." });
            }
            catch
            {
                return new ApiResponse<ConfirmationResponseDTO>(500, "An unexpected error occurred while updating the payment status.");
            }
        }
        #endregion

        #region CompleteCOD
        public async Task<ApiResponse<ConfirmationResponseDTO>> CompleteCODPaymentAsync(CODPaymentUpdateDTO cODPaymentUpdateDTO)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepo.Query()
                    .Include(o => o.Order)
                    .FirstOrDefaultAsync(
                        p => p.PaymentId == cODPaymentUpdateDTO.PaymentId &&
                             p.OrderId == cODPaymentUpdateDTO.OrderId);

                if (payment == null)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "payment not found");

                if (payment.Order == null)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "No Order in This payment");

                if (!IsCashOnDelivery(payment.paymentMethod))
                    return new ApiResponse<ConfirmationResponseDTO>(409, "Payment Method is not CashOnDelivery");

                if (payment.Order.OrderStatus != OrderStatus.Shipped)
                    return new ApiResponse<ConfirmationResponseDTO>(400, $"Order cannot be marked Delivered from '{payment.Order.OrderStatus}' state.");

                payment.paymentStatus = PaymentStatus.Completed;

                if (!string.IsNullOrWhiteSpace(cODPaymentUpdateDTO.transactionId))
                    payment.TransactionId = cODPaymentUpdateDTO.transactionId;

                payment.PaymentDate = DateTime.UtcNow;

                payment.Order.OrderStatus = OrderStatus.Delivered;

                _unitOfWork.PaymentRepo.Update(payment);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<ConfirmationResponseDTO>(200, "Success",
                    new ConfirmationResponseDTO { Message = $"COD payment for order {payment.Order.OrderId} completed and order marked as Delivered." });
            }
            catch
            {
                return new ApiResponse<ConfirmationResponseDTO>(500, "An Unexpected error Occurred While Completing COD");
            }
        }
        #endregion

        #region SendEmailOrder
        public async Task SendOrdeComfermationEmail(int orderID)
        {
            await SendOrderConfirmationEmailAsync(orderID);
        }
        #endregion

        #region Helpers
        private async Task<PaymentStatus> SimulatePaymentGateway()
        {
            await Task.Delay(1);
            int chance = Random.Shared.Next(1, 101);
            if (chance <= 60) return PaymentStatus.Completed;
            if (chance <= 90) return PaymentStatus.Pending;
            return PaymentStatus.Failed;
        }

        private string GenerateTransactionId()
        {
            return $"TXN-{Guid.NewGuid().ToString("N").ToUpper().Substring(0, 12)}";
        }

        private bool IsCashOnDelivery(PaymentMethod paymentMethod)
        {
            return paymentMethod == PaymentMethod.CashOnDelivery || paymentMethod == PaymentMethod.COD;
        }

        private void UpdateOrderStatusBasedOnPayment(Order order, Payment payment)
        {
            if (payment.paymentStatus == PaymentStatus.Completed)
                order.OrderStatus = OrderStatus.Processing;
            else if (payment.paymentStatus == PaymentStatus.Failed)
                order.OrderStatus = OrderStatus.Failed; 
            else if (payment.paymentStatus == PaymentStatus.Pending)
                order.OrderStatus = OrderStatus.Pending;
            else if (payment.paymentStatus == PaymentStatus.Refunded)
                order.OrderStatus = OrderStatus.Refunded;
        }

        public async Task SendOrderConfirmationEmailAsync(int orderId)
        {
            try
            {
                var order = await _unitOfWork.OrderRepo.Query()
                    .Include(o => o.User)
                    .Include(o => o.BillingAddress)
                    .Include(o => o.ShippingAddress)
                    .Include(o => o.Payment)
                    .Include(o => o.Items)
                        .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null) return;

                string subject = $"Order Confirmation - #{order.OrderId}";

                var itemsTable = string.Join("", order.Items.Select(i =>
                    $"<tr><td>{i.Product.Name}</td><td>{i.Quantity}</td><td>{i.Price:C}</td><td>{(i.Quantity * i.Price):C}</td></tr>"
                ));

                string emailBody = $@"
                    <h2>Hi {order.User.UserName},</h2>
                    <p>Thank you for your order <strong>#{order.OrderId}</strong> placed on {order.CreatedAt:dd MMM yyyy}.</p>

                    <h3>Order Details</h3>
                    <table border='1' cellspacing='0' cellpadding='5'>
                        <thead>
                            <tr><th>Product</th><th>Quantity</th><th>Price</th><th>Total</th></tr>
                        </thead>
                        <tbody>
                            {itemsTable}
                        </tbody>
                    </table>

                    <h3>Payment Info</h3>
                    <p>Method: {order.Payment?.paymentMethod}</p>
                    <p>Status: {order.Payment?.paymentStatus}</p>
                    <p>Total Amount: <strong>{order.TotalAmount:C}</strong></p>

                    <h3>Shipping Address</h3>
                    <p>{order.ShippingAddress?.PostalCode}, {order.ShippingAddress?.City}, {order.ShippingAddress?.Country}</p>
                ";

                await _EmailService.SendEmailAsync(order.User.Email, subject, emailBody, isBodyHtml: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }
        }
        #endregion
    }
}
