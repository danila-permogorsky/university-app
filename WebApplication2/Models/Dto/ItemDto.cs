using System.ComponentModel.DataAnnotations;

namespace Final.Models.Dto;

public class ItemDto
{
	public int? Id { get; set; }
	
	[Required]
	[Range(00000,99999, ErrorMessage = "UIN has only 5 digits")]
	public int Uin { get; set; }
	[Required]
	[StringLength(32,ErrorMessage = "Title length can't be more than 32.")]
	public string Name { get; set; }
	[Required]
	[StringLength(32,ErrorMessage = "Title length can't be more than 32.")]
	public string Material { get; set; }
	[Required]
	public string InstallStatus { get; set; }
	[Required]
	public string WarehouseStatus { get; set; }
}