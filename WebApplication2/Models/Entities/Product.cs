using Final.Models;
using Final.Models.Interfaces;

namespace WebApplication2.Models.Entities;

public enum AssemblyStatus
{
	Waiting = 0,
	InProgress = 1,
	Paused = 2,
	Done = 3
}

public class Product : IWithId
{
	public int Id { get; set; }
	public int SerialNumber { get; set; }
	public string Name { get; set; }
	public AssemblyStatus AssemblyStatus { get; set; }
	
	public ICollection<ItemProduct> ItemProducts { get; set; }
}