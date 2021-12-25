using Final.Models.Interfaces;

namespace WebApplication2.Models.Entities;

public enum InstallStatus
{
	Installed,
	PendingInstallation,
	InstallationProhibited
}

public enum WarehouseStatus
{
	InStock = 0,
	Missing = 1,
	NeedToOrder = 2	
}

public class Item: IWithId
{
	public int Id { get; set; }
	public int Uin { get; set; }
	public string Name { get; set; }
	public string Material { get; set; }
	public InstallStatus InstallStatus { get; set; }
	public WarehouseStatus WarehouseStatus { get; set; }
	
	public ICollection<ItemProduct> ItemProducts { get; set; }
}