using AutoMapper;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.DTO.CategoryDTOs;
using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace E_commerc_Servers.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CategoryServices( UnitOfWork unitOfWork, IMapper mapper )
        {
         _mapper= mapper;
         _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            try
            {
                if(categoryDto == null)
                {
                    return new ApiResponse<CreateCategoryDto>
                    {
                        StatusCode = 400,
                        Message = "Category data is required.",
                        Data = null
                    };
                }
                var normalizedName = categoryDto.Name?.Trim().ToLower();

                var exist= await _unitOfWork .CategoryRepo.Query().AnyAsync(c => c.Name.ToLower() == normalizedName);
                if (exist)
                {
                    return new ApiResponse<CreateCategoryDto>
                    {
                        StatusCode = 400,
                        Message = "Category with this name already exists.",
                        Data = null
                    };
                }
                if (categoryDto.ParentCategoryId.HasValue)
                {
                    var parent = await _unitOfWork.CategoryRepo.Query()
                        .AnyAsync(c => c.CategoryId == categoryDto.ParentCategoryId.Value);
                    if (!parent)
                    {
                        return new ApiResponse<CreateCategoryDto>
                        {
                            StatusCode = 404,
                            Message = "Parent category not found.",
                            Data = null
                        };
                    }
                }

                var category = _mapper.Map<Category>(categoryDto);
                await _unitOfWork.CategoryRepo.AddAsync(category);
                await _unitOfWork.SaveChangesAsync();

                var createdCategoryDto = _mapper.Map<CreateCategoryDto>(category);

                return new ApiResponse<CreateCategoryDto>(201, "Category created successfully.", createdCategoryDto);

            }
            catch (Exception ex)
            {
                return new ApiResponse<CreateCategoryDto>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while creating the category: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepo.Query().FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category == null)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = 404,
                        Message = "Category not found.",
                        Data = false
                    };
                }
                // Check if the category has any child categories
                var hasChildren = await _unitOfWork.CategoryRepo.Query().AnyAsync(c => c.ParentCategoryId == id);
                if (hasChildren)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = 400,
                        Message = "Cannot delete category with child categories.",
                        Data = false
                    };
                }
                // Check if the category has any products
                var hasProducts = await _unitOfWork.ProductRepo.Query().AnyAsync(p => p.CategoryId == id);
                if (hasProducts)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = 400,
                        Message = "Cannot delete category with associated products.",
                        Data = false
                    };
                }
                // Delete the category
                await _unitOfWork.CategoryRepo.Delete(id); // Pass the category ID instead of the category object
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>(200, "Category deleted successfully.", true);

            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while deleting the category: {ex.Message}",
                    Data = false
                };
            }
        }
        public async Task<ApiResponse<bool>> DeleteCategoryCascadeAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepo.Query()
                    .Include(c => c.Children)
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.CategoryId == id);

                if (category == null)
                    return new ApiResponse<bool>(404, "Category not found.", false);


                var uncategorized = await _unitOfWork.CategoryRepo.Query()
                    .FirstOrDefaultAsync(c => c.Name == "Uncategorized");

                if (uncategorized == null)
                {
                    uncategorized = new Category
                    {
                        Name = "Uncategorized",
                        Description = "Default category for unassigned products",

                    };
                    await _unitOfWork.CategoryRepo.AddAsync(uncategorized);
                    await _unitOfWork.SaveChangesAsync();
                }


                if (category.Children != null && category.Children.Any())
                {
                    foreach (var child in category.Children)
                    {
                        child.ParentCategoryId = category.ParentCategoryId;
                    }
                }


                if (category.Products != null && category.Products.Any())
                {
                    foreach (var product in category.Products)
                    {
                        product.CategoryId = uncategorized.CategoryId;
                    }
                }

                // 4️⃣ حذف الكاتيجوري
                _unitOfWork.CategoryRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<bool>(200, "Category deleted successfully. Children reassigned and products moved to 'Uncategorized'.", true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500, $"An error occurred while deleting the category: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<IEnumerable<CagtegoryResponesDto>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories =await _unitOfWork.CategoryRepo.Query().Include(c => c.Children)
                    .ToListAsync();
                if (categories == null || !categories.Any())
                {
                    return new ApiResponse<IEnumerable<CagtegoryResponesDto>>
                    {
                        StatusCode = 404,
                        Message = "No categories found.",
                        Data = null
                    };

                }
                var categoryDtos = _mapper.Map<IEnumerable<CagtegoryResponesDto>>(categories);
                return new ApiResponse<IEnumerable<CagtegoryResponesDto>>
                {
                    StatusCode = 200,
                    Message = "Categories retrieved successfully.",
                    Data = categoryDtos
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<CagtegoryResponesDto>>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving categories: {ex.Message}",
                    Data = null
                };
            }
        }

        public  async Task<ApiResponse<CagtegoryResponesDto>> GetCategoryByIdAsync(int id)

        {
            try
            { 
                var  category=await _unitOfWork.CategoryRepo.
                    Query()
                    .Include(category=>category.Children)
                    .Include(category => category.Products).FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category == null)
                    return new ApiResponse<CagtegoryResponesDto>
                    {
                        StatusCode = 404,
                        Message = "Category not found.",
                        Data = null
                    };
                var categoryDto = _mapper.Map<CagtegoryResponesDto>(category);
                return new ApiResponse<CagtegoryResponesDto>
                {
                    StatusCode = 200,
                    Message = "Category retrieved successfully.",
                    Data = categoryDto
                };


            }
            catch (Exception ex) 
            {
                return new ApiResponse<CagtegoryResponesDto>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving the category: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<ApiResponse<CategoryDetailsDto>> GetCategoryDetailsByIdAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepo.Query()
                    .Include(c => c.ParentCategory) 
                    .Include(c => c.Children).
                    Include(c => c.Products).
                    FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category == null)
                {
                    return new ApiResponse<CategoryDetailsDto>
                    {
                        StatusCode = 404,
                        Message = "Category not found.",
                        Data = null
                    };
                }
                var categoryDetailsDto = _mapper.Map<CategoryDetailsDto>(category);
                return new ApiResponse<CategoryDetailsDto>
                {
                    StatusCode = 200,
                    Message = "Category details retrieved successfully.",
                    Data = categoryDetailsDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryDetailsDto>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving the category details: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<bool>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
        {
            try 
            {
                var category= await _unitOfWork.CategoryRepo.Query().FirstOrDefaultAsync(C=>C.CategoryId == id);
                if (category == null) 
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = 404,
                        Message="Category not found",
                        Data = false
                    };
                }
                var normalizedName = categoryDto.Name?.Trim().ToLower();
                var exist = await _unitOfWork.CategoryRepo.Query()
                    .AnyAsync(c => c.Name.ToLower() == normalizedName && c.CategoryId != id);
                if (exist)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = 400,
                        Message = "Category with this name already exists.",
                        Data = false
                    };

                }
                if (categoryDto.ParentCategoryId.HasValue)
                {
                    var parentExists = await _unitOfWork.CategoryRepo.Query()
                        .AnyAsync(c => c.CategoryId == categoryDto.ParentCategoryId.Value);
                    if (!parentExists)
                    {
                        return new ApiResponse<bool>
                        {
                            StatusCode = 404,
                            Message = "Parent category not found.",
                            Data = false
                        };
                    }
                }
                // Map the updated properties from categoryDto to the existing category
                
                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;
                category.ParentCategoryId = categoryDto.ParentCategoryId.HasValue ?categoryDto.ParentCategoryId.Value : null;
                // Update the category in the repository
                _unitOfWork.CategoryRepo.Update(category);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    StatusCode = 200,
                    Message = "Category updated successfully.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving the category details: {ex.Message}",
                    Data = false
                };
            }
          
        }

      

    }
}
