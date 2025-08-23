using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.CartDtos;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerc_Servers.Services
{
    public class ShoppingCartServices : ICartServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ShoppingCartServices(UnitOfWork unitofwork,IMapper mapper,UserManager<User> usermanger )
        {
            _unitOfWork = unitofwork;
            _mapper = mapper;
            _userManager = usermanger;

        }
        public async Task<ApiResponse<CartResponesDto>> AddtoCartAsync(AddtocartDto addToCartDto)
        {
            try
            {
                if (addToCartDto == null)
                    return new ApiResponse<CartResponesDto> { StatusCode = 400, Message = "Invalid Cart Data", Data = null };
                var product = await _unitOfWork.ProductRepo.GetById(addToCartDto.ProductId);
                if (product == null || !product.IsActive)
                {
                    return new ApiResponse<CartResponesDto> { StatusCode = 404, Message = "Product not found", Data = null };
                }
                if (product.StockQuantity < addToCartDto.Quantity)
                {
                    return new ApiResponse<CartResponesDto> { StatusCode = 400, Message = "Insufficient stock", Data = null };
                }
                var cart = await _unitOfWork.ShoppingCartRepo.Query().Include(c => c.Items).
                      FirstOrDefaultAsync(u => u.UserName == addToCartDto.UserName && !u.ischeckedout);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserName = addToCartDto.UserName
                    };
                    await _unitOfWork.ShoppingCartRepo.AddAsync(cart);
                }
                var existingItems= cart.Items.FirstOrDefault(u=>u.ProductId== addToCartDto.ProductId);
                if (existingItems != null)
                {
                    existingItems.Quantity += addToCartDto.Quantity;
                    existingItems.Price = product.Price;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        ProductId = addToCartDto.ProductId,
                        Quantity = addToCartDto.Quantity,
                        Price = product.Price
                    });
                }
                await _unitOfWork.SaveChangesAsync();

                var respones = await ProjectCartForUserAsync(addToCartDto.UserName);
                if (respones != null) 
                {
                    return new ApiResponse<CartResponesDto>
                    (
                      200,
                      "Item Added to cart",
                       respones.Data )
                    ;
                }
                return new ApiResponse<CartResponesDto>
                {
                    StatusCode = 500,
                    Message = " SameThing went Worng",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartResponesDto>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }


        public async Task<ApiResponse<CartResponesDto>> UpdateCartItemAsync(UpdateCartitemDto updataDto)
        {
            try
            {
                if (updataDto == null)
                    return new ApiResponse<CartResponesDto>(400, "Invaid PayLoad ", null);
                var cart = await _unitOfWork.ShoppingCartRepo.Query().Include(s => s.Items)
                    .FirstOrDefaultAsync(u => u.UserName == updataDto.UserName && !u.ischeckedout);
                if (cart == null)
                    return new ApiResponse<CartResponesDto>(404, "Cart Not Found", null);
                var items = cart.Items.FirstOrDefault(u => u.CartItemId == updataDto.CartItemId);
                if (items == null) return new ApiResponse<CartResponesDto>(404, "Cart items Not Found");
                if (updataDto.Quantity < 1)
                    return new ApiResponse<CartResponesDto>(400, "Quantity must be At least one");
                var product = await _unitOfWork.ProductRepo.GetById(items.ProductId);
                if (product == null || !product.IsActive)
                    return new ApiResponse<CartResponesDto>(404, "Product Is Not Found or Unactive", null);
                if (product.StockQuantity < updataDto.Quantity)
                    return new ApiResponse<CartResponesDto>(400, "This Product Quntity UnAvailable", null);
                items.Quantity = updataDto.Quantity;
                await _unitOfWork.SaveChangesAsync();

                var reapones = await ProjectCartForUserAsync(updataDto.UserName);
                return new ApiResponse<CartResponesDto>(200, "Cart item Updated", reapones.Data);


            }
            catch(Exception ex)
            {
                return new ApiResponse<CartResponesDto>(500, ex.Message, null);
            }
        }

        public async Task<ApiResponse<CartResponesDto>> GetCartAsync(string userName)
        {
            try
            {
                var cart = await ProjectCartForUserAsync(userName);
                if (
                    cart == null)
                    return new ApiResponse<CartResponesDto>(404, "Cart With this user not found",null);

                return new ApiResponse<CartResponesDto>
                {
                    StatusCode = 200,
                    Message = "Cart retrieved",
                    Data = cart.Data
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<CartResponesDto>(500, ex.Message, null);
            }
        }

        public async Task<ApiResponse<CartResponesDto>> RemoveCartItemDto(RemoveCartItemDto removeCartItemDto)
        {
            try
            {
                if (removeCartItemDto == null)
                    return new ApiResponse<CartResponesDto>(400, "Invalid Payload", null);
                var cart = await _unitOfWork.ShoppingCartRepo.Query().Include(u => u.Items)
                    .FirstOrDefaultAsync(u => u.UserName == removeCartItemDto.UserName && !u.ischeckedout);


                if (cart == null)
                    return new ApiResponse<CartResponesDto>(404, "Active cart not found for this user", null);

                var items = cart.Items.FirstOrDefault(u=>u.CartItemId== removeCartItemDto.cartItemId);
                if (items == null)
                    return new ApiResponse<CartResponesDto>(404, "cart items not found", null);

                cart.Items.Remove(items);
                await _unitOfWork.SaveChangesAsync();

                var respones = await ProjectCartForUserAsync(removeCartItemDto.UserName);
                return new ApiResponse<CartResponesDto>
                {
                    StatusCode = 200,
                    Message = "Items removed Sucssccfully ",
                    Data = respones.Data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartResponesDto>(500, ex.Message, null);
            }
        }

  


         // This method is intended to project the cart for a specific user.
        public async Task<ApiResponse<CartResponesDto>> ProjectCartForUserAsync(string userName)
        {
            var cart = await _unitOfWork.ShoppingCartRepo.Query().
                Where(u => u.UserName == userName && !u.ischeckedout)
                .ProjectTo<CartResponesDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if(cart==null)
            {
                return new ApiResponse<CartResponesDto>
                {
                    StatusCode = 404,
                    Message = "Cart not found for the user",
                    Data = null
                };
            }
            return new ApiResponse<CartResponesDto>
            {
                StatusCode =200,
                Message = "Cart retrieved successfully",
                Data = cart
            };

        }

    }
}
