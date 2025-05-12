using System.ComponentModel.DataAnnotations;
using Techan.Models.Common;

namespace Techan.ViewModels.Brands
{
	public class BrandUpdateVM:BaseEntity
	{
		[MinLength(3)]
		public string Name { get; set; }
		public IFormFile? ImageFile { get; set; }
		public string? ImageUrl {  get; set; }
	}
}
