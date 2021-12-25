using AutoMapper;
using Final.Models.Dto;
using Final.Models.ItemViewModels;
using Final.Models.Services;
using Final.Models.ViewModels.ItemViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers;

public class ItemsController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IMapper _mapper;
	private readonly IItemService _itemService;

	public ItemsController(ILogger<HomeController> logger, IMapper mapper, IItemService itemService)
	{
		_logger = logger;
		_mapper = mapper;
		_itemService = itemService;
	}

	[HttpGet]
	public IActionResult Index()
	{
		var items = _mapper.Map<IEnumerable<ItemDto>, IEnumerable<ItemViewModel>>(_itemService.GetAll());
		return View(items);
	}

	[HttpGet]
	public async Task<IActionResult> Info(int? id)
	{
		if (id == null)
			return NotFound();

		var viewModel = _mapper.Map<ItemViewModel>( await _itemService.GetItemAsync((int) id));

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

		var editModel = _mapper.Map<EditItemViewModel>(await _itemService.GetItemAsync((int) id));

		if (editModel == null)
			return NotFound();

		return View(editModel);
	}

	[HttpGet]
	public IActionResult Delete(int? id)
	{
		if (id == null)
			return NotFound();

		var deleteModel = _mapper.Map<DeleteItemViewModel>(_itemService.Delete((int) id));

		if (deleteModel == null)
			return NotFound();

		return View(deleteModel);
	}
	
	[HttpPost]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateItemViewModel inputModel)
	{
		if (!ModelState.IsValid) return View(inputModel);

		await _itemService.Add(_mapper.Map<ItemDto>(inputModel));
		
		return RedirectToAction(nameof(Index));
	}
			

	[HttpPost]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public IActionResult Edit(int id, EditItemViewModel editViewModel)
	{
		if (!ModelState.IsValid) return View(editViewModel);

		var detail = _mapper.Map<ItemDto>(editViewModel);
		detail.Id = id;

		var result = _itemService.Update(detail);

		if (result == null)
			return NotFound();

		return RedirectToAction(nameof(Index));
	}

	[HttpPost, ActionName("Delete")]
	// [Authorize(Roles = "Admin")]
	// [ValidateAntiForgeryToken]
	public IActionResult DeleteConfirmed(int id)
	{
		var detail = _itemService.Delete(id);

		if (detail == null)
			return NotFound();
			
		_logger.LogTrace($"Detail with {detail.Id} has been deleted");
		return RedirectToAction(nameof(Index));
	}
}