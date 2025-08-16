using AutoMapper;
using E_commerce_Core.DTO.CategoryDTOs;
using E_commerce_Core.Entityes;

namespace E_commerce_Core.MappingProfile
{
    public class CategoryProfile: Profile
    {

        public CategoryProfile()
        {
            CreateMap<Category, CagtegoryResponesDto>()
             .ForMember(d=>d.ParentName, opt => opt.MapFrom(s => s.ParentCategory != null ? s.ParentCategory.Name : "No Parent"))
             .ForMember(d => d.ChildrenNames, opt => opt.MapFrom(s => s.Children != null ? s.Children.Select(c => c.Name).ToList() : new List<string>()));

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<UpdateCategoryDto,Category>();

            CreateMap<Category, CategoryDetailsDto>()
    .ForMember(d => d.ParentCategory,
        opt => opt.MapFrom(s => s.ParentCategory != null
            ? new ParentCategoryDto
            {
                CategoryId = s.ParentCategory.CategoryId,
                Name = s.ParentCategory.Name
            }
            : null))
    .ForMember(d => d.Children,
        opt => opt.MapFrom(s => s.Children != null
            ? s.Children.Select(c => new ChildCategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name
            }).ToList()
            : new List<ChildCategoryDto>()))
    .ForMember(d => d.Products,
        opt => opt.MapFrom(s => s.Products != null
            ? s.Products.Select(p => new ProductResponseDto
            {
                ProductId = p.ProductId,
                ProductName = p.Name,
                Price = p.Price,
                Rating = p.Rating
            }).ToList()
            : new List<ProductResponseDto>()));

        }


    }
}
