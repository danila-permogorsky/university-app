using AutoMapper;
using Final.Models.Dto;
using Final.Models.ItemViewModels;
using Final.Models.ViewModels.ItemViewModels;

namespace WebApplication2.Models.ViewModels.ViewModelsProfiles;

public class ItemViewModelProfile : Profile
{
	public ItemViewModelProfile()
	{
		CreateMap<ItemDto, CreateItemViewModel>().ReverseMap();
		CreateMap<ItemDto, DeleteItemViewModel>().ReverseMap();
		CreateMap<ItemDto, EditItemViewModel>().ReverseMap();
		CreateMap<ItemDto, ItemViewModel>();
	}
}