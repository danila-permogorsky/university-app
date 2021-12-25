using AutoMapper;
using Final.Controllers;
using Final.Models.Dto;
using Final.Models.Services;
using Final.Models.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public class ProductsController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IMapper _mapper;
	private readonly IProductService _productService;

	public ProductsController(ILogger<HomeController> logger, IMapper mapper, IProductService productService)
	{
		_logger = logger;
		_mapper = mapper;
		_productService = productService;
	}

	[HttpGet]
	public IActionResult Index()
	{
		var products = _mapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductViewModel>>(_productService.GetAll());
		return View(products);
	}

	[HttpGet]
	public async Task<IActionResult> Info(int? id)
	{
		if (id == null)
			return NotFound();

		var viewModel = _mapper.Map<ProductViewModel>( await _productService.GetProductAsync((int) id));

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

		var editModel = _mapper.Map<EditProductViewModel>(await _productService.GetProductAsync((int) id));

		if (editModel == null)
			return NotFound();

		return View(editModel);
	}

	[HttpGet]
	public IActionResult Delete(int? id)
	{
		if (id == null)
			return NotFound();

		var deleteModel = _mapper.Map<DeleteProductViewModel>(_productService.Delete((int) id));

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

		await _productService.Add(_mapper.Map<ProductDto>(inputModel));
		
		return RedirectToAction(nameof(Index));
	}
			

	[HttpPost]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public IActionResult Edit(int id, EditProductViewModel editViewModel)
	{
		if (!ModelState.IsValid) return View(editViewModel);

		var product = _mapper.Map<ProductDto>(editViewModel);
		product.Id = id;

		var result = _productService.Update(product);

		if (result == null)
			return NotFound();

		return RedirectToAction(nameof(Index));
	}

	[HttpPost, ActionName("Delete")]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public IActionResult DeleteConfirmed(int id)
	{
		var detail = _productService.Delete(id);

		if (detail == null)
			return NotFound();
			
		_logger.LogTrace($"Detail with {detail.Id} has been deleted");
		return RedirectToAction(nameof(Index));
	}
}