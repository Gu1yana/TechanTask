using System.ComponentModel.DataAnnotations;
using Techan.Models;
using Techan.Models.Common;
using Techan.ViewModels.CommonVM;

namespace Techan.ViewModels.Categories
{
	public class CategoryUpdateVM: BaseVM
    {
        [MinLength(5), MaxLength(64), Required]
        public string Name { get; set; }
		public IEnumerable<Product>? Products { get; set; }
	}
}