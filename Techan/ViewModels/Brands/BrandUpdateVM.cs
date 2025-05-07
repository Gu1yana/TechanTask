using Techan.Models.Common;

namespace Techan.ViewModels.Brands
{
	public class BrandUpdateVM:BaseEntity
	{
		public string Name { get; set; }
		public IFormFile ImageFile { get; set; }
		public string ImageUrl {  get; set; }
	}
}
