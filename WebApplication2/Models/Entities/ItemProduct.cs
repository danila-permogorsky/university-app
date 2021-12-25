using Final.Models;

namespace WebApplication2.Models.Entities;

public class ItemProduct
{
	public int ItemId { get; set; }
	public Item Item { get; set; }
	
	public int ProductId { get; set; }
	public Product Product { get; set; }
}