
using E_commerce_Core.DTO.AddresDtos;
using E_commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _addressServices;

        public AddressController( IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }

        [HttpPost("CreateAddress")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto createAddressDto_)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _addressServices.CreateAddressAsync(createAddressDto_);

            if (response.StatusCode == 201)
            {
                return CreatedAtAction(nameof(GetAddressById), new { id = response.Data.Id }, response.Data);
            }
            else
            {
                return StatusCode(response.StatusCode, response.Message);
            }
        }

        [HttpGet("GetAddressById/{id}")]
        public async Task<IActionResult>GetAddressById(int id)
        {
            var response=await _addressServices.GetAddressByIdAsync(id);
            if(response.StatusCode is >= 200 and< 300)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound(response.Message);
            }
        }

        [HttpGet("GetAllAddresses")]
        public async Task<IActionResult> GetAllAddresses()
        {
            var response = await _addressServices.GetAllAddressesAsync();
            if (response.StatusCode is >= 200 and < 300)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound(response.Message);
            }

        }
        [HttpPut("UpdateAddress/{id}")]
        public async Task<IActionResult>UpdateAddress(int id , [FromBody] UpdateAddressDto updateAddressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var  response  =await _addressServices.UpdateAddressAsync(id, updateAddressDto);
            if (response.StatusCode  is >= 200 and < 300)
            {
                return Ok(response.Data);
            }
            else
            {
                return StatusCode(response.StatusCode, response.Message);
            }
        }
        [HttpDelete("DeleteAddress/{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var response = await _addressServices.DeleteAddressAsync(id);
            if (response.StatusCode is >= 200 and < 300)
            {
                return Ok(response.Data);
            }
            else
            {
                return StatusCode(response.StatusCode, response.Message);
            }
        }
        [HttpGet("GetAddressesByUserName/{UserName}")]
        public async Task<IActionResult > GetAddressByUserName(string UserName)
        {
            var respones = await _addressServices .GetAddressesByUserNameAsync(UserName);
            if (respones.StatusCode is >= 200 and  < 300)
            {
                return Ok(respones.Data);
            }
            else
            {
                return NotFound(respones.Message);
            }
        }
    }
}
