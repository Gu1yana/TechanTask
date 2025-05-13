using System.ComponentModel.DataAnnotations;
using Techan.Models;
using Techan.Models.Common;

namespace Techan.ViewModels.Categories
{
	public class CategoryGetVM:BaseEntity
	{
        [MinLength(5), MaxLength(64), Required]
        public string Name {  get; set; }
		public IEnumerable<Product>? Products { get; set; }
	}
}
