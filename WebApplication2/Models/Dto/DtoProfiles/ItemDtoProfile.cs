using AutoMapper;
using WebApplication2.Models.Entities;

namespace Final.Models.Dto.DtoProfiles;

public class ItemDtoProfile : Profile
{
	public ItemDtoProfile()
	{
		CreateMap<Item, ItemDto>()
			.ForMember(destination => destination.InstallStatus,
				opt => opt.MapFrom(source => Enum.GetName(typeof(InstallStatus), source.InstallStatus)))
			.ForMember(destination => destination.WarehouseStatus,
				opt => opt.MapFrom(source => Enum.GetName(typeof(WarehouseStatus), source.WarehouseStatus)))
			.ReverseMap();
	}
}