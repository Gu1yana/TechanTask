using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Techan.DataAccessLayer;
using Techan.Extensions;
using Techan.Models;
using Techan.ViewModels.Brands;
using Techan.ViewModels.Categories;
using Techan.ViewModels.Products;
using Techan.ViewModels.Sliders;

namespace Techan.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Authorize(Roles = "Superadmin, Admin,Moderator")]
	public class ProductController(TechanDbContext _context) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var products = await _context.Products.Select(x => new ProductListVM
			{
				Id = x.Id,
				Name = x.Name,
				BrandName = x.Brand.Name,
				CategoryName = x.Category.Name,
				CostPrice = x.CostPrice,
				SellPrice = x.SellPrice,
				Discount = x.Discount,
				ImageUrl = x.ImageUrl,
				IsDeleted=x.IsDeleted
			}).ToListAsync();
			return View(products);
		}
		public async Task<IActionResult> Create()
		{
			await ViewBags();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductCreateVM model)
		{
			if (model.ImageFile != null)
			{
				if (!model.ImageFile.IsValidType("image"))
					ModelState.AddModelError("ImageFile", "File must be an image");
				if (!model.ImageFile.IsValidSize(200))
					ModelState.AddModelError("ImageFile", "File size must be equal or less than 200kb");
			}
			if (model.ImageFiles != null)
			{
				if (!model.ImageFiles.All(x => x.IsValidType("image")))
					ModelState.AddModelError("ImageFiles", "Files must be image");
				if (!model.ImageFiles.All(x => x.IsValidSize(200)))
					ModelState.AddModelError("ImageFiles", "Files size must be equal or less than 200kb");
			}
			if (!ModelState.IsValid)
			{
			    await ViewBags();
				return View(model);
			}
			Product product=new Product()
			{
			Name = model.Name,
			Description = model.Description,
			CostPrice = model.CostPrice,
			SellPrice= model.SellPrice,
			ImageUrl = await model.ImageFile!.UploadAsync(Path.Combine("wwwroot","imgs","products")),
			BrandId = model.BrandId,
			CategoryId = model.CategoryId,	
			};
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (!id.HasValue || id.Value < 1) return BadRequest();
			int result = await _context.Products.Where(x => x.Id == id).ExecuteDeleteAsync();
			if (result == 0)
				return NotFound();
			return RedirectToAction(nameof(Index));
		}
        public async Task<IActionResult> SoftDeleted(int? id)
        {
            if (!id.HasValue || id < 1) return BadRequest();
            var model = await _context.Brands.FindAsync(id);
            if (model is null)
                return NotFound();
            model.IsDeleted = !model.IsDeleted;
			await _context.SaveChangesAsync();
			TempData["IsDeleted"] = true;
			return RedirectToAction(nameof(Index));
        }

        private async Task ViewBags()
		{
			var brands = await _context.Brands.Select(
					x => new BrandGetVM
					{
						Id = x.Id,
						Name = x.Name,
					}).ToListAsync();

			var categories = await _context.Categories.Select(
				x => new CategoryGetVM
				{
					Id = x.Id,
					Name = x.Name,
				}).ToListAsync();

			ViewBag.Brands = brands;
			ViewBag.Categories = categories;
		}
	}
}