using System.ComponentModel.DataAnnotations;
using Techan.Models.Common;
using Techan.ViewModels.CommonVM;

namespace Techan.ViewModels.Brands
{
	public class BrandUpdateVM: BaseVM
    {
		[MinLength(3)]
		public string Name { get; set; }
		public IFormFile? ImageFile { get; set; }
		public string? ImageUrl {  get; set; }
	}
}
