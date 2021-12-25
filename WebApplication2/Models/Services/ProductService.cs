using System.Data.Common;
using AutoMapper;
using Final.Data;
using Final.Models.Dto;
using Final.Models.Services;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Entities;

namespace WebApplication2.Models.Services;

public class ProductService : IProductService
{
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;

	public ProductService(ApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	public async Task<ProductDto> GetProductAsync(int id)
	{
		return _mapper.Map<ProductDto>( await _context.Products.FirstOrDefaultAsync(i => i.Id == id));

	}

	public IEnumerable<ProductDto> GetAll()
	{
		return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(_context.Products.ToList());

	}

	public async Task<ProductDto> Update(ProductDto productDto)
	{
		try
		{
			var item = _mapper.Map<Product>(productDto);

			_context.Update(item);
			await _context.SaveChangesAsync();

			return _mapper.Map<ProductDto>(item);

		}
		catch (DbUpdateException e)
		{
			Console.WriteLine($"Update error: {productDto}");
			throw;
		}
	}

	public async Task<ProductDto> Add(ProductDto productDto)
	{
		var item = _context.Add(_mapper.Map<Product>(productDto)).Entity;
		await _context.SaveChangesAsync();
		return _mapper.Map<ProductDto>(item);
	}

	public async Task<ProductDto> Delete(int id)
	{
		var item = await _context.Products.FindAsync(id);

		try
		{
			if (item == null)
				throw new ArgumentNullException($"Deleting view model is null {item}");

			_context.Products.Remove(item);
			await _context.SaveChangesAsync();

			return _mapper.Map<ProductDto>(item);
		}
		catch (DbException e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}