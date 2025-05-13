using System.ComponentModel.DataAnnotations;
using Techan.Models;
using Techan.Models.Common;

namespace Techan.ViewModels.Products;

public class ProductListVM : BaseEntity
{
	public string Name { get; set; }
	public string BrandName { get; set; }
	public string CategoryName { get; set; }
	public string Description { get; set; }
	[Range(0, 100)]
	public byte Discount { get; set; }
	public decimal CostPrice { get; set; }
	public decimal SellPrice { get; set; }
	public string ImageUrl { get; set; }
	public bool IsDeleted { get; set; }
}
