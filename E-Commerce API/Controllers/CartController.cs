using E_commerce_Core.DTO.CartDtos;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartServices _cartServices;
    private readonly ICheckoutServices _CheckoutServices;

    public CartController(ICartServices cartServices, ICheckoutServices services)
    {
        _cartServices = cartServices;
        _CheckoutServices = services;
    }

    [HttpPost("AddToCart")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> AddToCart([FromBody] AddtocartDto addtocart)
    {
        var response = await _cartServices.AddtoCartAsync(addtocart);
        return StatusCode(response.StatusCode, response);
    }

   
    [HttpGet("GetCart/{username}")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetCart([FromRoute] string username)
    {
        var response = await _cartServices.GetCartAsync(username);
        return StatusCode(response.StatusCode, response);
    }

   
    [HttpPut("UpdateItem")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> UpdateItem([FromBody] UpdateCartitemDto updateCartitemDto)
    {
        var response = await _cartServices.UpdateCartItemAsync(updateCartitemDto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("RemoveItem")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> RemoveItem([FromBody] RemoveCartItemDto removeCartItemDto)
    {
        var response = await _cartServices.RemoveCartItemDto(removeCartItemDto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("Checkout")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDTO checkoutRequest)
    {
        var response = await _CheckoutServices.CheckoutAsync(checkoutRequest);
        return StatusCode(response.StatusCode, response);
    }
}
