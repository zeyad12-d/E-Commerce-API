using E_commerce_Core.DTO.CategoryDTOs;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryResponse = await _categoryServices.CreateCategoryAsync(createCategoryDto);

            if (categoryResponse.StatusCode >= 200 && categoryResponse.StatusCode < 300)
                return StatusCode(categoryResponse.StatusCode, categoryResponse);

            return StatusCode(categoryResponse.StatusCode, categoryResponse);
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryServices.DeleteCategoryAsync(id);

            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return StatusCode(response.StatusCode, response);

            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("DeleteCategoryCascade/{id}")]
        public async Task<IActionResult> DeleteCascade(int id)
        {
            var response = await _categoryServices.DeleteCategoryCascadeAsync(id);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return StatusCode(response.StatusCode, response);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult>GetAllCategore()
        {
            var response = await _categoryServices.GetAllCategoriesAsync();
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return StatusCode(response.StatusCode, response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<IActionResult>GetCategoryById(int id)
        {
            var respones = await _categoryServices.GetCategoryByIdAsync(id);
            if(respones.StatusCode>=200&&respones.StatusCode<300)
                return StatusCode(respones.StatusCode, respones);

            return StatusCode(respones.StatusCode, respones);
        }
        [HttpGet("GetCategoryDetailsById/{id}")]
        public async Task<IActionResult> GetCategoryDetailsById(int id)
        {
            var response = await _categoryServices.GetCategoryDetailsByIdAsync(id);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return StatusCode(response.StatusCode, response);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("UpdateCategory/{id}")]
        public  async Task<IActionResult >UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _categoryServices.UpdateCategoryAsync(id, updateCategoryDto);
            if (response.StatusCode >= 200 && response.StatusCode < 300)
                return StatusCode(response.StatusCode, response);
            return StatusCode(response.StatusCode, response);
        }
    }
}
