using System.ComponentModel.DataAnnotations;

namespace Final.Models.ViewModels.ProductViewModels;

public class CreateProductViewModel
{
	[Required]
	[Range(0000,9999, ErrorMessage = "Serial number has only 4 digits")]
	public int SerialNumber { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public string AssemblyStatus { get; set; }
}