using System.Data.Common;
using AutoMapper;
using Final.Data;
using Final.Models.Dto;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Entities;

namespace Final.Models.Services;

public class ItemService : IItemService
{
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;

	public ItemService(ApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<ItemDto> GetItemAsync(int id)
	{
		return _mapper.Map<ItemDto>( await _context.Items.FirstOrDefaultAsync(i => i.Id == id));
	}

	public IEnumerable<ItemDto> GetAll()
	{
		return _mapper.Map<IEnumerable<Item>, IEnumerable<ItemDto>>(_context.Items.ToList());
	}

	public async Task<ItemDto> Update(ItemDto itemDto)
	{
		try
		{
			var item = _mapper.Map<Item>(itemDto);

			_context.Update(item);
			await _context.SaveChangesAsync();

			return _mapper.Map<ItemDto>(item);

		}
		catch (DbUpdateException e)
		{
			Console.WriteLine($"Update error: {itemDto}");
			throw;
		}
	}

	public async Task<ItemDto> Add(ItemDto itemDto)
	{
		var item = _context.Add(_mapper.Map<Item>(itemDto)).Entity;
		await _context.SaveChangesAsync();
		return _mapper.Map<ItemDto>(item);
	}

	public async Task<ItemDto> Delete(int id)
	{
		var item = await _context.Items.FindAsync(id);

		try
		{
			if (item == null)
				throw new ArgumentNullException($"Deleting view model is null {item}");

			_context.Items.Remove(item);
			await _context.SaveChangesAsync();

			return _mapper.Map<ItemDto>(item);
		}
		catch (DbException e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}