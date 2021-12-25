using AutoMapper;
using Final.Models.Dto;
using Final.Models.ViewModels.ProductViewModels;

namespace WebApplication2.Models.ViewModels.ViewModelsProfiles;

public class ProductViewModelProfile : Profile
{
	public ProductViewModelProfile()
	{
		CreateMap<ProductDto, CreateProductViewModel>().ReverseMap();
		CreateMap<ProductDto, DeleteProductViewModel>();
		CreateMap<ProductDto, EditProductViewModel>().ReverseMap();
		CreateMap<ProductDto, ProductViewModel>();
	}
}