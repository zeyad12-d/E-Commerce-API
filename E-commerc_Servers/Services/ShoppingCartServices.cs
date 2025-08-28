using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.CartDtos;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerc_Servers.Services
{
    public class ShoppingCartServices : ICartServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ShoppingCartServices(UnitOfWork unitofwork, IMapper mapper, UserManager<User> usermanger)
        {
            _unitOfWork = unitofwork;
            _mapper = mapper;
            _userManager = usermanger;
        }

        #region AddTocart
        public async Task<ApiResponse<CartResponesDto>> AddtoCartAsync(AddtocartDto addToCartDto)
        {
            try
            {
                if (addToCartDto == null)
                    return new ApiResponse<CartResponesDto>(400, "Invalid Cart Data", null);

                // Get User
                var user = await _userManager.FindByNameAsync(addToCartDto.UserName);
                if (user == null)
                    return new ApiResponse<CartResponesDto>(404, "User not found");
                var userId = user.Id;

                // Get Product
                var product = await _unitOfWork.ProductRepo.GetById(addToCartDto.ProductId);
                if (product == null || !product.IsActive)
                    return new ApiResponse<CartResponesDto>(404, "Product not found", null);

                if (product.StockQuantity < addToCartDto.Quantity)
                    return new ApiResponse<CartResponesDto>(400, "Insufficient stock", null);

                // Get or Create Cart
                var cart = await _unitOfWork.ShoppingCartRepo.Query()
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(u => u.userId == userId && !u.ischeckedout);

                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        userId = userId,
                        Items = new List<CartItem>()
                    };
                    await _unitOfWork.ShoppingCartRepo.AddAsync(cart);
                }

                // Add or Update CartItem
                var existingItems = cart.Items.FirstOrDefault(u => u.ProductId == addToCartDto.ProductId);
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

                // Response
                var respones = await ProjectCartForUserAsync(userId);
                return new ApiResponse<CartResponesDto>(200, "Item Added to cart", respones.Data);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                throw new Exception("Save failed: " + msg, ex);
            }
        }
        #endregion

        #region UpdateCartItem
        public async Task<ApiResponse<CartResponesDto>> UpdateCartItemAsync(UpdateCartitemDto updataDto)
        {
            try
            {
                if (updataDto == null)
                    return new ApiResponse<CartResponesDto>(400, "Invalid Payload", null);

                var user = await _userManager.FindByNameAsync(updataDto.UserName);
                if (user == null)
                    return new ApiResponse<CartResponesDto>(404, "User not found");
                var userId = user.Id;

                var cart = await _unitOfWork.ShoppingCartRepo.Query()
                    .Include(s => s.Items)
                    .FirstOrDefaultAsync(u => u.userId == userId && !u.ischeckedout);

                if (cart == null)
                    return new ApiResponse<CartResponesDto>(404, "Cart Not Found", null);

                var items = cart.Items.FirstOrDefault(u => u.CartItemId == updataDto.CartItemId);
                if (items == null)
                    return new ApiResponse<CartResponesDto>(404, "Cart items Not Found");

                if (updataDto.Quantity < 1)
                    return new ApiResponse<CartResponesDto>(400, "Quantity must be at least one");

                var product = await _unitOfWork.ProductRepo.GetById(items.ProductId);
                if (product == null || !product.IsActive)
                    return new ApiResponse<CartResponesDto>(404, "Product Is Not Found or Unactive", null);

                if (product.StockQuantity < updataDto.Quantity)
                    return new ApiResponse<CartResponesDto>(400, "This Product Quantity UnAvailable", null);

                items.Quantity = updataDto.Quantity;
                await _unitOfWork.SaveChangesAsync();

                var respones = await ProjectCartForUserAsync(userId);
                return new ApiResponse<CartResponesDto>(200, "Cart item Updated", respones.Data);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartResponesDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region GetCartItems
        public async Task<ApiResponse<CartResponesDto>> GetCartAsync(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    return new ApiResponse<CartResponesDto>(404, "User not found");

                var cart = await ProjectCartForUserAsync(user.Id);
                if (cart == null || cart.Data == null)
                    return new ApiResponse<CartResponesDto>(404, "Cart With this user not found", null);

                return new ApiResponse<CartResponesDto>(200, "Cart retrieved", cart.Data);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartResponesDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region RemoveCartItem
        public async Task<ApiResponse<CartResponesDto>> RemoveCartItemDto(RemoveCartItemDto removeCartItemDto)
        {
            try
            {
                if (removeCartItemDto == null)
                    return new ApiResponse<CartResponesDto>(400, "Invalid Payload", null);

                var user = await _userManager.FindByNameAsync(removeCartItemDto.UserName);
                if (user == null)
                    return new ApiResponse<CartResponesDto>(404, "User not found");
                var userId = user.Id;

                var cart = await _unitOfWork.ShoppingCartRepo.Query()
                    .Include(u => u.Items)
                    .FirstOrDefaultAsync(u => u.userId == userId && !u.ischeckedout);

                if (cart == null)
                    return new ApiResponse<CartResponesDto>(404, "Active cart not found for this user", null);

                var items = cart.Items.FirstOrDefault(u => u.CartItemId == removeCartItemDto.cartItemId);
                if (items == null)
                    return new ApiResponse<CartResponesDto>(404, "Cart item not found", null);

                cart.Items.Remove(items);
                await _unitOfWork.SaveChangesAsync();

                var respones = await ProjectCartForUserAsync(userId);
                return new ApiResponse<CartResponesDto>(200, "Item removed Successfully", respones.Data);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartResponesDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Helper
        public async Task<ApiResponse<CartResponesDto>> ProjectCartForUserAsync(string userId)
        {
            var cart = await _unitOfWork.ShoppingCartRepo.Query()
                .Where(u => u.userId == userId && !u.ischeckedout)
                .ProjectTo<CartResponesDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (cart == null)
                return new ApiResponse<CartResponesDto>(404, "Cart not found for the user", null);

            return new ApiResponse<CartResponesDto>(200, "Cart retrieved successfully", cart);
        }

       
        #endregion
    }
}
