using E_commerce_Core.DTO.CategoryDTOs;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpPost("AddCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryResponse = await _categoryServices.CreateCategoryAsync(createCategoryDto);

            return StatusCode(categoryResponse.StatusCode, categoryResponse);
        }

        
        [HttpDelete("DeleteCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryServices.DeleteCategoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }

      
        [HttpDelete("DeleteCategoryCascade/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCascade(int id)
        {
            var response = await _categoryServices.DeleteCategoryCascadeAsync(id);
            return StatusCode(response.StatusCode, response);
        }

       
        [HttpGet("GetAllCategories")]
        [AllowAnonymous] 
        public async Task<IActionResult> GetAllCategore()
        {
            var response = await _categoryServices.GetAllCategoriesAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCategoryById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var respones = await _categoryServices.GetCategoryByIdAsync(id);
            return StatusCode(respones.StatusCode, respones);
        }

   
        [HttpGet("GetCategoryDetailsById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryDetailsById(int id)
        {
            var response = await _categoryServices.GetCategoryDetailsByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        
        [HttpPut("UpdateCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _categoryServices.UpdateCategoryAsync(id, updateCategoryDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
