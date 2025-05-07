using Techan.Models;

namespace Techan.ViewModels.Categories
{
	public class CategoryUpdateVM
	{
		public string Name { get; set; }
		public IEnumerable<Product>? Products { get; set; }
	}
}
