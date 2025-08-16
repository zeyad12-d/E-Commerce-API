using E_commerc_Servers.Services.DTO.ProductDto;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;

        public ProductController(IProductServices productServics )
        {
            _productService = productServics;
        }

        // Create
        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductCreateDto productDto)
        {
            var response = await _productService.CreateProductAsync(productDto);
            if (response.Success)
            {
                return Created(
             $"/api/Product/GetProductById/{response.Data.ProductId}",
             response.Data);
            };
            return BadRequest(response.Message);
        }
        // Get All Products
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProductsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _productService.GetAllProductsAsync(pageNumber, pageSize);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        // Get Product By Id
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }
        // Get All Products By Category
        [HttpGet("GetAllProductsByCategory/{categoryId}")]

        public async Task<IActionResult> GetAllProductsByCategoryAsync(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _productService.GetAllProductsByCategoryAsync(categoryId, pageNumber, pageSize);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }
        // Search Products
        [HttpGet("SearchProducts")]
        public async Task<IActionResult> SearchProductsAsync(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _productService.SearchProductsAsync(searchTerm, pageNumber, pageSize);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }
        // Update Product
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] ProductUpdateDto productDto)
        {
            var response = await _productService.UpdateProductAsync(id, productDto);
            if (response.Success)
            {
                return NoContent();
            }
            return BadRequest(response.Message);
        }
        // Update Product Status
        [HttpPut("UpdateProductStatus/{id}")]
        public async Task<IActionResult> UpdateProductStatusAsync(int id, [FromBody] bool isActive)
        {
            var response = await _productService.UpdateProductStatusAsync(id, isActive);
            if (response.Success)
            {
                return NoContent();
            }
            return BadRequest(response.Message);
        }
        // Delete Product
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var response = await _productService.DeleteProductAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            return NotFound(response.Message);
        }
    }

}
