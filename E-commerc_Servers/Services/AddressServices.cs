using AutoMapper;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.AddresDtos;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerc_Servers.Services
{
   
    public class AddressServices : IAddressServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AddressServices( UnitOfWork unitofwork, UserManager<User> userManager ,IMapper mapper )
        {
            _mapper = mapper;
            _unitOfWork = unitofwork;
            _userManager = userManager;
        }
        public async Task<ApiResponse<AddressResponseDto>> CreateAddressAsync(CreateAddressDto createAddressDto)
        {
            try 
            { 
                var user = await _userManager.FindByNameAsync(createAddressDto.UserName);
                if (user == null)
                {
                    return new ApiResponse<AddressResponseDto>
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                var address = _mapper.Map<Address>(createAddressDto);
                address.UserId = user.Id;

                await _unitOfWork.AddressRepo.AddAsync(address);
                await _unitOfWork.SaveChangesAsync();
                var addressResponse = _mapper.Map<AddressResponseDto>(address);
                return new ApiResponse<AddressResponseDto>
                {
                    StatusCode = 201,
                    Message = "Address created successfully",
                    Data = addressResponse
                };

            }
            catch (Exception ex)
            {
                return  new ApiResponse<AddressResponseDto>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteAddressAsync(int id)
        {
            try
            {
                var address=  await _unitOfWork .AddressRepo .Query().FirstOrDefaultAsync(a => a.Id == id);
                if (address == null)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = 404,
                        Message = "Address not found",
                        Data = false
                    };
                }
             await _unitOfWork.AddressRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    StatusCode = 200,
                    Message = "Address deleted successfully",
                    Data = true
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<AddressResponseDto>> GetAddressByIdAsync(int id)
        {
            try
            {
                var address = await _unitOfWork.AddressRepo.Query().Include(u=>u.user).FirstOrDefaultAsync(a => a.Id == id);

                if (address == null)
                {
                    return new ApiResponse<AddressResponseDto>
                    {
                        StatusCode = 404,
                        Message = "Address not found"
                    };
                }

                var addressResponse= _mapper.Map<AddressResponseDto>(address);

                return new ApiResponse<AddressResponseDto>
                {
                    StatusCode = 200,
                    Message = "Address retrieved successfully",
                    Data = addressResponse
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<AddressResponseDto>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<AddressResponseDto>>> GetAddressesByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new ApiResponse<IEnumerable<AddressResponseDto>>
                    {
                        StatusCode = 404,
                        Message = "User not found",
                        Data = null
                    };
                }
                var address= await _unitOfWork .AddressRepo.Query().Where(user=>user.user.UserName == userName)
                    .Include(u => u.user)
                    .ToListAsync();
                if (!address.Any())
                {
                    return new ApiResponse<IEnumerable<AddressResponseDto>>
                    {
                        StatusCode = 404,
                        Message = "No addresses found for this user",
                        Data = null
                    };
                }
               var addressResponses = _mapper.Map<IEnumerable<AddressResponseDto>>(address);
                return new ApiResponse<IEnumerable<AddressResponseDto>>
                {
                    StatusCode = 200,
                    Message = "Addresses retrieved successfully",
                    Data = addressResponses
                };
            }
            catch (Exception ex )
            {
                return new ApiResponse<IEnumerable<AddressResponseDto>>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<AddressResponseDto>>> GetAllAddressesAsync()
        {
            try 
            {
                var address = await _unitOfWork.AddressRepo.Query().Include(u => u.user)
                    .ToListAsync();
                if  (!address.Any())
                {
                    return new ApiResponse<IEnumerable<AddressResponseDto>>
                    {
                        StatusCode = 404,
                        Message = "No addresses found",
                        Data = null
                    };
                }

                var addressResponses= _mapper.Map<IEnumerable<AddressResponseDto>>(address);

                return new ApiResponse<IEnumerable<AddressResponseDto>>
                {
                    StatusCode = 200,
                    Message = "Addresses retrieved successfully",
                    Data = addressResponses
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<IEnumerable<AddressResponseDto>>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<AddressResponseDto>> UpdateAddressAsync(int id, UpdateAddressDto updateAddressDto)
        {
            try 
            {
                var address = await _unitOfWork.AddressRepo.Query().FirstOrDefaultAsync(a => a.Id == id);
                if (address == null)
                {
                    return new ApiResponse<AddressResponseDto>
                    {
                        StatusCode = 404,
                        Message = "Address not found"
                    };
                }
                var updatedAddress = _mapper.Map(updateAddressDto, address);
                
                _unitOfWork.AddressRepo.Update(updatedAddress);
                await _unitOfWork.SaveChangesAsync();


                var addressResponse = _mapper.Map<AddressResponseDto>(updatedAddress);

                return new ApiResponse<AddressResponseDto>
                {
                    StatusCode = 200,
                    Message = "Address updated successfully",
                    Data = addressResponse
                };
            }

            catch (Exception ex)
            {
              return  new ApiResponse<AddressResponseDto>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
}
