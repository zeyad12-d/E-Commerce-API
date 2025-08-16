using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_commerc_Servers.Services.DTO.ProductDto;
using E_commerce_Core.ApiRespones;
using E_commerce_Core.Interfaces.Services;
using E_commerce_Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace E_commerc_Servers.Services
{
    public class ProductServices : IProductServices
    {
        public readonly UnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public ProductServices(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    public async Task<ApiResponse<GetAllProductDto>> CreateProductAsync(ProductCreateDto productDto)
        {
            try
            {

                var exisit = await _unitOfWork.ProductRepo.Query().AnyAsync(p => p.Name.ToLower() == productDto.Name.ToLower());
                if (exisit)
                {
                    return new ApiResponse<GetAllProductDto>(400, "Product with this name already exists", null);
                }
                var categoryExists = await _unitOfWork.CategoryRepo.Query().AnyAsync(c => c.CategoryId == productDto.CategoryId);

                if (!categoryExists)
                {
                    return new ApiResponse<GetAllProductDto>(404, "Category not found", null);
                }
               
                var product = _mapper.Map<ProductCreateDto, Product>(productDto);
                await _unitOfWork.ProductRepo.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();

                // بعد الحفظ، product.ProductId هيكون فيه القيمة الجديدة
                var dto = _mapper.Map<GetAllProductDto>(product);
                return new ApiResponse<GetAllProductDto>(200, "Product created successfully", dto);
            }
            catch (Exception ex) 
            {
                   return new ApiResponse<GetAllProductDto>(500, $"An error occurred while creating the product: {ex.Message}", null);
            }

        }
        public async Task<ApiResponse<IEnumerable<GetAllProductDto>>> GetAllProductsAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;
                var products = await _unitOfWork.ProductRepo
                    .Query()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).AsNoTracking()
                    .ToListAsync();
                if (products.Count == 0)
                {
                    return (new ApiResponse<IEnumerable<GetAllProductDto>>(404, "No products found", null));
                }

                var productDtos = _mapper.Map<IEnumerable<Product>, IEnumerable<GetAllProductDto>>(products);
                return (new ApiResponse<IEnumerable<GetAllProductDto>>(200, "Products retrieved successfully", productDtos));
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<GetAllProductDto>>(500, $"An error occurred while retrieving products: {ex.Message}", null);
            }
        }
        public async Task<ApiResponse<GetAllProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepo.Query().Where(p => p.ProductId == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return new ApiResponse<GetAllProductDto>(404, "Product not found", null);
            }
            var productDto = _mapper.Map<GetAllProductDto>(product);
            return new ApiResponse<GetAllProductDto>(200, "Product retrieved successfully", productDto);
        }
        public async Task<ApiResponse<IEnumerable<GetAllProductDto>>> GetAllProductsByCategoryAsync(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var products = await _unitOfWork.ProductRepo
                     .Query()
                    .Where(p => p.CategoryId == categoryId)
                     .Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .AsNoTracking()
                     .ToListAsync();
                if (!products.Any())
                {
                    return new ApiResponse<IEnumerable<GetAllProductDto>>(404, "No products found in this category", null);
                }
                var productDtos = _mapper.Map<IEnumerable<Product>, IEnumerable<GetAllProductDto>>(products);

                return new ApiResponse<IEnumerable<GetAllProductDto>>(200, "Products retrieved successfully", productDtos);
            }
            catch (Exception ex) 
            {
                return new ApiResponse<IEnumerable<GetAllProductDto>> { StatusCode = 500, Message = $"An error occurred while retrieving products by category: {ex.Message}", Data = null };

            }

        }
        public async Task<ApiResponse<IEnumerable<GetAllProductDto>>> SearchProductsAsync(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            // تنظيف مصطلح البحث
            var normalizedTerm = searchTerm?.Trim();
            if (string.IsNullOrWhiteSpace(normalizedTerm))
            {
                return new ApiResponse<IEnumerable<GetAllProductDto>>(400, "Search term is required", null);
            }

            try
            {
                var query = _unitOfWork.ProductRepo.Query()
                    .AsNoTracking()
                    .Where(p => EF.Functions.Like(p.Name, $"%{normalizedTerm}%"));

                var totalCount = await query.CountAsync();

                var products = await query
                    .OrderBy(p => p.ProductId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<GetAllProductDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new ApiResponse<IEnumerable<GetAllProductDto>>(
                    200,
                    products.Any() ? $"Found {totalCount} matching products" : "No products found",
                    products
                );
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<GetAllProductDto>>(
                    500,
                    $"An error occurred while searching for products: {ex.Message}",
                    null
                );
            }
        }
        public async Task<ApiResponse<bool>> UpdateProductAsync(int id, ProductUpdateDto productDto)
        {
            try
            {
                var product = await _unitOfWork.ProductRepo.Query().AsNoTracking().Where(p => p.ProductId == id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return new ApiResponse<bool>(404, "Product not found", false);
                }
                // Check if the name already exists for another product
                var nameExists = await _unitOfWork.ProductRepo.Query()
                    .AnyAsync(p => p.Name.ToLower() == productDto.Name.ToLower() && p.ProductId != id);
                if (nameExists)
                {
                    return new ApiResponse<bool>(400, "Product with this name already exists", false);
                }
                // Update product properties
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.CategoryId = productDto.CategoryId;
                product.IsActive = productDto.IsActive;
                _unitOfWork.ProductRepo.Update(product);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>(200, "Product updated successfully", true);

            }
            catch(Exception ex)
            {
                return new ApiResponse<bool>(500, "An error occurred while updating the product: " + ex.Message, false);
            }
        }
        public async Task<ApiResponse<bool>> UpdateProductStatusAsync(int id, bool isActive)
        {
            try
            {
                var product = await _unitOfWork.ProductRepo.Query().FirstOrDefaultAsync(p => p.ProductId == id);
                if (product == null)
                {
                    return new ApiResponse<bool>(404, "product not found", false);
                }

                product.IsActive = isActive;
                _unitOfWork.ProductRepo.Update(product);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>(200, "Product status updated successfully", true);


            }
            catch (Exception ex) 
            {
                return new ApiResponse<bool>(500, "An error occurred while updating product status: " + ex.Message, false);
            }
       }
        public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
        {
            var result = await UpdateProductStatusAsync(id, false);

            if (result.StatusCode == 404)
            {
                return new ApiResponse<bool>(404, "Product not found", false);
            }

            if (result.StatusCode != 200)
            {
                return new ApiResponse<bool>(500, "Error occurred while deleting product", false);
            }

            return new ApiResponse<bool>(200, "Product deleted successfully", true);
        }

    }
}
