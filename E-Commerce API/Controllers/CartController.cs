using E_commerce_Core.DTO.CartDtos;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartServices _cartServices;

    public CartController(ICartServices cartServices)
    {
        _cartServices = cartServices;
    }

    [HttpPost("AddToCart")]
    public async Task<IActionResult> AddToCart([FromBody] AddtocartDto addtocart)
    {
        var response = await _cartServices.AddtoCartAsync(addtocart);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetCart/{username}")]
    public async Task<IActionResult> GetCart([FromRoute] string username)
    {
        var response = await _cartServices.GetCartAsync(username);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateItem")]
    public async Task<IActionResult> UpdateItem([FromBody] UpdateCartitemDto updateCartitemDto)
    {
        var response = await _cartServices.UpdateCartItemAsync(updateCartitemDto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("RemoveItem")]
    public async Task<IActionResult> RemoveItem([FromBody] RemoveCartItemDto removeCartItemDto)
    {
        var response = await _cartServices.RemoveCartItemDto(removeCartItemDto);
        return StatusCode(response.StatusCode, response);
    }
}
