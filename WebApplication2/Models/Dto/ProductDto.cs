using System.ComponentModel.DataAnnotations;

namespace Final.Models.Dto;

public class ProductDto
{
	public int? Id { get; set; }
	[Required]
	[Range(0000,9999, ErrorMessage = "Serial number has only 4 digits")]
	public int SerialNumber { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public string AssemblyStatus { get; set; }
}