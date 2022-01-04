using AutoMapper;
using Final.Controllers;
using Final.Models.Dto;
using Final.Models.ItemViewModels;
using Final.Models.ViewModels.ItemViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Entities;
using WebApplication2.Models.Services;

namespace WebApplication2.Controllers;

public class ItemsController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IMapper _mapper;
	private readonly IService<Item, ItemDto> _service;

	public ItemsController(ILogger<HomeController> logger, IMapper mapper, IService<Item, ItemDto> service)
	{
		_logger = logger;
		_mapper = mapper;
		_service = service;
	}

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var items = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<ItemViewModel>>( await _service.GetAllAsync());
		return View(items);
	}

	[HttpGet]
	public async Task<IActionResult> Info(int? id)
	{
		if (id == null)
			return NotFound();
		
		var item = await _service.GetAsync((int) id);
		var model = _mapper.Map<ItemViewModel>(item);

		if (model == null)
			return NotFound();

		return View(model);
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


		var item = await _service.GetAsync((int) id);
		var model = _mapper.Map<EditItemViewModel>(item);

		if (model == null)
			return NotFound();

		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null)
			return NotFound();

		var model = _mapper.Map<DeleteItemViewModel>(await _service.GetAsync((int) id));
		
		if (model == null)
			return NotFound();

		return View(model);
	}
	
	[HttpPost]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateItemViewModel inputModel)
	{
		if (!ModelState.IsValid) return View(inputModel);
		
		await _service.SaveAsync(_mapper.Map<ItemDto>(inputModel));
		
		return RedirectToAction(nameof(Index));
	}
			

	[HttpPost]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, EditItemViewModel editViewModel)
	{
		if (!ModelState.IsValid) return View(editViewModel);

		var itemDto = _mapper.Map<ItemDto>(editViewModel);
		itemDto.Id = id;
		await _service.UpdateAsync(itemDto);

		return RedirectToAction(nameof(Index));
	}

	[HttpPost, ActionName("Delete")]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var item = await _service.DeleteAsync((int) id);

		_logger.LogTrace($"Item with {item.Id} has been deleted");
		return RedirectToAction(nameof(Index));
	}
}