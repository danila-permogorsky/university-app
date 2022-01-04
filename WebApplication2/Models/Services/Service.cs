using AutoMapper;
using Final.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.Services;

public class Service<TSource, TDestination> : IService<TSource, TDestination>
	where TSource : class, new()
	where TDestination : class, new()
{
	private readonly IMapper _mapper;
	private readonly ApplicationDbContext _context;

	public Service(IMapper mapper, ApplicationDbContext context)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<TDestination> GetAsync(int id)
		=> _mapper.Map<TDestination>(await _context.Set<TSource>().FindAsync(id));

	public async Task<IEnumerable<TDestination>> GetAllAsync()
		=> _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(await _context.Set<TSource>().ToListAsync());

	public async Task<TDestination> UpdateAsync(TDestination entity)
	{
		var test = _mapper.Map<TSource>(entity);
		_context.Set<TSource>().Update(test);
		await _context.SaveChangesAsync();
		return await Task.FromResult(entity);
	}

	public async Task<TDestination> SaveAsync(TDestination entity)
	{
		await _context.Set<TSource>().AddAsync(_mapper.Map<TSource>(entity));
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task<TDestination> DeleteAsync(int id)
	{ 
		var entity = await _context.Set<TSource>().FindAsync(id);

		try
		{
			if (entity == null)
				throw new ArgumentNullException($"Deleting entity is null {entity}");

			_context.Set<TSource>().Remove(entity);
			await _context.SaveChangesAsync();

			return _mapper.Map<TDestination>(entity);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}