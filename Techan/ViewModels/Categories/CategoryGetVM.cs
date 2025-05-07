using Techan.Models;
using Techan.Models.Common;

namespace Techan.ViewModels.Categories
{
	public class CategoryGetVM:BaseEntity
	{
		public string Name {  get; set; }
		public IEnumerable<Product>? Products { get; set; }
	}
}
