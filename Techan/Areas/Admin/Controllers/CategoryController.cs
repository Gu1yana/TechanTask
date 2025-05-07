using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Categories;
using Techan.ViewModels.Sliders;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(TechanDbContext _context) : Controller
    {
		public async Task<IActionResult> Index()
		{
			List<Category> datas = [];
			datas = await _context.Categories.ToListAsync();
			List<CategoryGetVM> vmc = [];
			foreach (var data in datas)
			{
				vmc.Add(new CategoryGetVM
				{
					Id = data.Id,
					Name = data.Name,
				});
			}
			return View(vmc);
		}
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CategoryCreateVM model)
		{
			if (!ModelState.IsValid)
				return View(model);
			Category category = new()
			{
			};
			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (!id.HasValue || id.Value < 1) return BadRequest();
			int result = await _context.Categories.Where(x => x.Id == id).ExecuteDeleteAsync();
			if (result == 0)
				return NotFound();
			return RedirectToAction(nameof(Index));
		}
	}
}
