using AutoMapper;
using Final.Controllers;
using Final.Models.Dto;
using Final.Models.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Entities;
using WebApplication2.Models.Services;

namespace WebApplication2.Controllers;

public class ProductsController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IMapper _mapper;
	private readonly IService<Product, ProductDto> _service;

	public ProductsController(ILogger<HomeController> logger, IMapper mapper, IService<Product, ProductDto> service)
	{
		_logger = logger;
		_mapper = mapper;
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var products = _mapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductViewModel>>( await _service.GetAllAsync());
		return View(products);
	}

	[HttpGet]
	public async Task<IActionResult> Info(int? id)
	{
		if (id == null)
			return NotFound();

		var viewModel = _mapper.Map<ProductViewModel>( await _service.GetAsync((int) id));

		if (viewModel == null)
			return NotFound();

		return View(viewModel);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null)
			return NotFound();

		var editModel = _mapper.Map<EditProductViewModel>(await _service.GetAsync((int) id));

		if (editModel == null)
			return NotFound();

		return View(editModel);
	}

	[HttpGet]
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null)
			return NotFound();

		var deleteModel = _mapper.Map<DeleteProductViewModel>( await _service.GetAsync((int) id));

		if (deleteModel == null)
			return NotFound();

		return View(deleteModel);
	}
	
	[HttpPost]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateProductViewModel inputModel)
	{
		if (!ModelState.IsValid) return View(inputModel);

		await _service.SaveAsync(_mapper.Map<ProductDto>(inputModel));
		
		return RedirectToAction(nameof(Index));
	}
			

	[HttpPost]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, EditProductViewModel editViewModel)
	{
		if (!ModelState.IsValid) return View(editViewModel);

		var product = _mapper.Map<ProductDto>(editViewModel);
		product.Id = id;

		await _service.UpdateAsync(product);

		return RedirectToAction(nameof(Index));
	}

	[HttpPost, ActionName("Delete")]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var detail = await _service.DeleteAsync(id);

		_logger.LogTrace($"Detail with {detail.Id} has been deleted");
		return RedirectToAction(nameof(Index));
	}
}