using Final.Models.Dto;

namespace Final.Models.Services;

public interface IProductService
{
	Task<ProductDto> GetProductAsync(int id);
	IEnumerable<ProductDto> GetAll();
	Task<ProductDto> Update(ProductDto productDto);
	Task<ProductDto> Add(ProductDto productDto);
	Task<ProductDto> Delete(int id);
}