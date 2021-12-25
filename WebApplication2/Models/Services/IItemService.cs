using Final.Models.Dto;

namespace Final.Models.Services;

public interface IItemService
{
	Task<ItemDto> GetItemAsync(int id);
	IEnumerable<ItemDto> GetAll();
	Task<ItemDto> Update(ItemDto itemDto);
	Task<ItemDto> Add(ItemDto itemDto);
	Task<ItemDto> Delete(int id);
}