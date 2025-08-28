using AutoMapper;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.CartDtos;
using E_commerce_Core.DTO.OrderDtos;
using E_commerce_Core.DTO.PaymentDtos;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerc_Servers.Services
{
    public class CheckoutServices : ICheckoutServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentServices _paymentService;
        private readonly UserManager<User> _userManager;
        public CheckoutServices(UnitOfWork unitOfWork, IMapper mapper, IPaymentServices services, UserManager<User> user)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = services;
            _userManager = user;
        }


        #region CheckOut
        public async Task<ApiResponse<OrderResponseDto>> CheckoutAsync(CheckoutRequestDTO checkoutRequest)
        {
            await using var transaction = await _unitOfWork._dbcontext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByNameAsync(checkoutRequest.UserName);
                if (user == null)
                    return new ApiResponse<OrderResponseDto>(404, "User Not found");

                var userid = user.Id;
                var cart = await _unitOfWork.ShoppingCartRepo.Query()
                    .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.userId == userid && !c.ischeckedout);

                if (cart == null || !cart.Items.Any())
                    return new ApiResponse<OrderResponseDto>(404, "Cart is empty", null);

                // 1) Create Pending Order
                var order = new Order
                {
                    UserName = checkoutRequest.UserName,
                    UserId = userid,
                    OrderStatus = OrderStatus.Pending,
                    TotalAmount = cart.Items.Sum(i => i.Price * i.Quantity),
                    CreatedAt = DateTime.UtcNow,
                    Items = cart.Items.Select(i => new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                };
                await _unitOfWork.OrderRepo.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                // 2) Reserve Stock
                foreach (var item in cart.Items)
                {
                    if (item.Product.StockQuantity < item.Quantity)
                        return new ApiResponse<OrderResponseDto>(400, $"Product '{item.Product.Name}' out of stock.");

                    item.Product.StockQuantity -= item.Quantity;
                    _unitOfWork.ProductRepo.Update(item.Product);
                }
                await _unitOfWork.SaveChangesAsync();

                // 3) Call Payment Service
                var paymentResult = await _paymentService.ProcessPaymentAsync(new PaymentRequestDTO
                {
                    OrderId = order.OrderId,
                    Amount = order.TotalAmount,
                    PaymentMethod = checkoutRequest.PaymentMethod
                });

                if (!paymentResult.Success)
                {
                    // Compensation: rollback stock + cancel order
                    foreach (var item in cart.Items)
                    {
                        item.Product.StockQuantity += item.Quantity;
                        _unitOfWork.ProductRepo.Update(item.Product);
                    }
                    order.OrderStatus = OrderStatus.Cancelled;
                    _unitOfWork.OrderRepo.Update(order);

                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new ApiResponse<OrderResponseDto>(400, "Payment failed", null);
                }

                // 4) Confirm Order
                order.OrderStatus = OrderStatus.Confirmed;
                _unitOfWork.OrderRepo.Update(order);

                var payment = new Payment
                {
                    Order = order,
                    Amount = order.TotalAmount,
                    paymentMethod = checkoutRequest.PaymentMethod,
                    PaymentDate = DateTime.UtcNow,
                    paymentStatus = PaymentStatus.Completed
                };
                await _unitOfWork.PaymentRepo.AddAsync(payment);

                cart.ischeckedout = true;

                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                await _paymentService.SendOrdeComfermationEmail(order.OrderId);

                var response = _mapper.Map<OrderResponseDto>(order);
                return new ApiResponse<OrderResponseDto>(200, "Checkout successful", response);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<OrderResponseDto>(500, $"Checkout failed: {ex.Message}", null);
            }
        }

        #endregion

      

    }
}

