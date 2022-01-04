namespace WebApplication2.Models.Services;

public interface IService<TSource, TDestination> 
	where TSource : class, new()
	where TDestination : class, new()
{
	Task<TDestination> GetAsync(int id);
	Task<IEnumerable<TDestination>> GetAllAsync();
	Task<TDestination> UpdateAsync(TDestination entity);
	Task<TDestination> SaveAsync(TDestination entity);
	Task<TDestination> DeleteAsync(int id);
}