using AutoMapper;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.OrderDtos;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerc_Servers.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public OrderServices( UnitOfWork unitOfWork ,IMapper mapper ,UserManager<User> user  )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
            _userManager = user;
        }

        public async Task<ApiResponse<OrderResponseDto>> CreateOrderAsync(CreateOrderDto orderDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(orderDto.UserName);
                if (user == null)
                {
                    return new ApiResponse<OrderResponseDto>
                    {
                        StatusCode = 404,
                        Message = "User not found.",
                        Data = null
                    };
                }

                var shippingAddress = await _unitOfWork.AddressRepo.GetById(orderDto.ShippingAddressId);
                var billingAddress = await _unitOfWork.AddressRepo.GetById(orderDto.BillingAddressId);

                if (shippingAddress == null)
                    return new ApiResponse<OrderResponseDto>
                    {
                        StatusCode = 400,
                        Message = "Shipping address is invalid.",
                        Data = null
                    };

                if (billingAddress == null)
                    return new ApiResponse<OrderResponseDto>
                    {
                        StatusCode = 400,
                        Message = "Billing address is invalid.",
                        Data = null
                    };

                var productIds = orderDto.OrderItems.Select(i => i.ProductId).ToList();
                var products = await _unitOfWork.ProductRepo.Query()
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToListAsync();

                if (products.Count != productIds.Count)
                    return new ApiResponse<OrderResponseDto>
                    {
                        StatusCode = 404,
                        Message = "One or more products not found.",
                        Data = null
                    };

                foreach (var item in orderDto.OrderItems)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (product == null || product.StockQuantity < item.Quantity)
                    {
                        return new ApiResponse<OrderResponseDto>
                        {
                            StatusCode = 400,
                            Message = $"Product {product?.Name ?? "Unknown"} is out of stock or insufficient quantity.",
                            Data = null
                        };
                    }
                }

                var order = new Order
                {
                    UserId = user.Id,
                    UserName = orderDto.UserName,
                    ShoppingAddressId = orderDto.ShippingAddressId,
                    BillingAddressId = orderDto.BillingAddressId,
                    CreatedAt = DateTime.UtcNow,
                    OrderStatus = "Pending",
                    Items = new List<OrderItem>()
                };

                decimal totalAmount = 0;
                foreach (var item in orderDto.OrderItems)
                {
                    var product = products.First(p => p.ProductId == item.ProductId);

                    var orderItem = new OrderItem
                    {
                        ProductId = product.ProductId,
                        Quantity = item.Quantity,
                        Price = product.Price * item.Quantity
                    };

                    totalAmount += orderItem.Price;

                    order.Items.Add(orderItem);

                    product.StockQuantity -= item.Quantity;
                    _unitOfWork.ProductRepo.Update(product);
                }

                order.TotalAmount = totalAmount;

                await _unitOfWork.OrderRepo.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                var orderResponse = _mapper.Map<OrderResponseDto>(order);

                return new ApiResponse<OrderResponseDto>
                {
                    StatusCode = 201,
                    Message = "Order created successfully.",
                    Data = orderResponse
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderResponseDto>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var order = await _unitOfWork.OrderRepo.Query()
                    .AsNoTracking()
                    .Where(o => o.OrderId == orderId)
                    .Select(o => new OrderResponseDto
                    {
                        Id = o.OrderId,
                        UserId = o.User.Id,
                        UserName = o.User.UserName,
                        ShippingAddress = o.ShippingAddress.AddressLine1 + ", " + o.ShippingAddress.City + ", " + o.ShippingAddress.Country,
                        BillingAddress = o.BillingAddress.AddressLine1 + ", " + o.BillingAddress.City + ", " + o.BillingAddress.Country,
                        OrderDate = o.CreatedAt,
                        OrderStatus = o.OrderStatus,
                        TotalAmount = o.TotalAmount,
                        OrderItems = o.Items.Select(item => new OrderItemResponseDto
                        {
                            ProductId = item.ProductId,
                            ProductName = item.Product.Name,
                            Quantity = item.Quantity,
                            Price = item.Price
                        }).ToList()
                    }).FirstOrDefaultAsync();

                if (order == null)
                {
                    return new ApiResponse<OrderResponseDto>
                    {
                        StatusCode = 404,
                        Message = "Order not found.",
                        Data = null
                    };
                }

                return new ApiResponse<OrderResponseDto>
                {
                    StatusCode = 200,
                    Message = "Order retrieved successfully.",
                    Data = order
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderResponseDto>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetAllOrdersAsync(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var totalOrders = await _unitOfWork.OrderRepo.Query().CountAsync();

            var respones = await _unitOfWork.OrderRepo.Query()
                .Include(o => o.User)
                .Include(o => o.ShippingAddress)
                .Include(o => o.BillingAddress)
                .Include(o => o.Items).ThenInclude(i => i.Product)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .Select(s => new OrderResponseDto
                {
                    Id = s.OrderId,
                    OrderStatus = s.OrderStatus,
                    TotalAmount = s.TotalAmount,
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    ShippingAddress = s.ShippingAddress.AddressLine1 + ", " + s.ShippingAddress.City + ", " + s.ShippingAddress.Country,
                    BillingAddress = s.BillingAddress.AddressLine1 + ", " + s.BillingAddress.City + ", " + s.BillingAddress.Country,
                    OrderDate = s.CreatedAt,
                    OrderItems = s.Items.Select(c => new OrderItemResponseDto
                    {
                        ProductId = c.ProductId,
                        ProductName = c.Product.Name,
                        Price = c.Price,
                        Quantity = c.Quantity
                    }).ToList()
                }).ToListAsync();

            return new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                StatusCode = 200,
                Message = respones.Any() ? "Orders retrieved successfully." : "No orders found.",
                Data = respones,
                TotalPages = (int)Math.Ceiling((double)totalOrders / pageSize)
            };
        }

        public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetOrdersByUserNameAsync(string userName)
        {
            try 
            {
                var user= await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new ApiResponse<IEnumerable<OrderResponseDto>>
                    {
                        StatusCode = 404,
                        Message = "User not found.",
                        Data = null
                    };

                }
                var orders = await _unitOfWork .OrderRepo.Query().
                  Include(o => o.User)
                  .Include(o => o.ShippingAddress)
                  .Include(o => o.BillingAddress)
                  .Include(o => o.Items)
                  .ThenInclude(i => i.Product)
                  .AsNoTracking()
                  .Where(o => o.UserId == user.Id)
                  .Select(s=> new OrderResponseDto
                  {
                      OrderStatus =s.OrderStatus,
                      TotalAmount = s.TotalAmount,
                      UserId = s.UserId,
                      UserName = s.User.UserName,
                      ShippingAddress = s.ShippingAddress.AddressLine1 + ", " + s.ShippingAddress.City + ", " + s.ShippingAddress.Country,
                      BillingAddress = s.BillingAddress.AddressLine1 + ", " + s.BillingAddress.City + ", " + s.BillingAddress.Country,
                      OrderDate = s.CreatedAt,
                      OrderItems = s.Items.Select(c => new OrderItemResponseDto
                      {
                          ProductId = c.ProductId,
                          ProductName = c.Product.Name,
                          Price = c.Price,
                          Quantity = c.Quantity
                      }).ToList()

                  }).ToListAsync();

                return new ApiResponse<IEnumerable<OrderResponseDto>>
                {
                    StatusCode = 200,
                    Message = orders.Any() ? "Orders retrieved successfully." : "No orders found for this user.",
                    Data = orders
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<OrderResponseDto>>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving orders: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<decimal>> CalculateTotalAmountAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepo.Query().Include(o => o.Items).FirstOrDefaultAsync(s => s.OrderId == orderId);

            if (order == null)
            {
                return new ApiResponse<decimal>
                {
                    StatusCode = 404,
                    Message = " Order Not Found",
                    Data = 0
                };

            }
            var totalAmount = order.TotalAmount;

            return new ApiResponse<decimal>
            {
                StatusCode = 200,
                Message = " data reseved ",
                Data = totalAmount
            };

       }

        public async Task<ApiResponse<bool>> DeleteOrderAsync(int orderId)
        {

            try
            {
                var order = await _unitOfWork .OrderRepo.Query().FirstOrDefaultAsync(c=>c.OrderId == orderId);
                if (order ==null)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode =404,
                        Message =" order not found",
                        Data=false
                    };
                }
                await _unitOfWork.OrderRepo.Delete(order.OrderId).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
                return new ApiResponse<bool>
                {
                    StatusCode = 200,
                    Message = " order Deleted",
                    Data = true
                };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<bool>
                {
                    StatusCode  = 500,
                    Message = ex.Message,
                    Data = false
                };
            }
        }

     public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetOrdersByStatusAsync(string status  ,int pageNumber , int pageSize)
        { 
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var totalNumber = await _unitOfWork.OrderRepo.Query().CountAsync();

             var order = await _unitOfWork.OrderRepo.Query()
                .Include(C=>C.User).Include(C => C.ShippingAddress).
                Include(C => C.Items)
                .ThenInclude(C => C.Product)
                .AsNoTracking()
                .Where(c => c.OrderStatus.Trim().ToLower() == status.Trim().ToLower())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new OrderResponseDto
                {
                    OrderStatus = s.OrderStatus,
                    TotalAmount = s.TotalAmount,
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    ShippingAddress = s.ShippingAddress.AddressLine1 + ", " + s.ShippingAddress.City + ", " + s.ShippingAddress.Country,
                    BillingAddress = s.ShippingAddress.AddressLine1 + ", " + s.ShippingAddress.City + ", " + s.ShippingAddress.Country,
                    OrderDate = s.CreatedAt,
                    OrderItems = s.Items.Select(c => new OrderItemResponseDto
                    {
                        ProductId = c.ProductId,
                        ProductName = c.Product.Name,
                        Price = c.Price,
                        Quantity = c.Quantity
                    }).ToList()
                }).ToListAsync();

            return new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                StatusCode = 200,
                Message = order.Any() ? "Orders retrieved successfully." : "No orders found with the specified status.",
                Data = order,
                TotalPages = (int)Math.Ceiling((double)totalNumber / pageSize)
            };
        }

       

        public async Task<ApiResponse<bool>> UpdateOrderStatusAsync(int orderId, string status)
        {
            try 
            {
                var order = await _unitOfWork.OrderRepo.Query().FirstOrDefaultAsync(o => o.OrderId == orderId);
                if (order == null)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = 404,
                        Message = "Order not found.",
                        Data = false
                    };
                }
                order.OrderStatus = status;
                 _unitOfWork.OrderRepo.Update(order);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    StatusCode = 200,
                    Message = "Order status updated successfully.",
                    Data = true
                };
            }
            catch ( Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while updating the order status.{ex.Message}",
                    Data = false
                };
            }
        }
    }
}
