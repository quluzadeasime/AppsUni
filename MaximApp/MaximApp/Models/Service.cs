
using System.ComponentModel.DataAnnotations.Schema;

namespace MaximApp.Models;

public class Service
{
	public int Id { get; set; }
	[Required]
	[StringLength(45,ErrorMessage ="Max uzunluq 45 ola biler!")]
	public string Title { get; set; }
	[Required]
	[StringLength(150, ErrorMessage = "Max uzunluq 150 ola biler!")]
	public string Description { get; set; }	
	public string? ImgUrl {  get; set; }
	[NotMapped]
	public IFormFile? ImgFile { get; set; }
}
