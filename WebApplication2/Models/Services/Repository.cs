using System.Linq.Expressions;
using Final.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.Services;

public class Repository<T> : IRepository<T>
	where T : class, new()
{
	private readonly ApplicationDbContext _context;

	public Repository(ApplicationDbContext context)
	{
		this._context = context;
	}

	public async Task<T> Get(int id) 
		=> await _context.Set<T>().FindAsync(id) ?? throw new InvalidOperationException();

	public async Task<T> Find(Expression<Func<T, bool>> predicate)
		=> await _context.Set<T>().FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();

	public async Task<IEnumerable<T>> GetAll()
		=> await _context.Set<T>().ToListAsync();

	public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
		=> await _context.Set<T>().Where(predicate).ToListAsync();

	public void Add(T entity)
		=> _context.Set<T>().Add(entity);

	public void AddRange(IEnumerable<T> entities)
		=> _context.Set<T>().AddRange(entities);

	public void Update(T entity)
		=> _context.Set<T>().Update(entity);

	public void UpdateRange(IEnumerable<T> entities)
		=> _context.Set<T>().UpdateRange(entities);

	public void Delete(T entity)
		=> _context.Set<T>().Remove(entity);

	public void DeleteRange(IEnumerable<T> entities)
		=> _context.Set<T>().RemoveRange(entities);
}