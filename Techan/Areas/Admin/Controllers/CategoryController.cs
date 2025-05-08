using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
			Category category = new();
			category.Name = model.Name;
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
		public async Task<IActionResult> Update(int? id)
		{
			if (id.HasValue && id < 1) return BadRequest();
			var entity = await _context.Categories.Select(x => new CategoryUpdateVM { Id = x.Id, Name = x.Name }).FirstOrDefaultAsync(x => x.Id == id);
			if (entity is null) return NotFound();
			return View(entity);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int?id, CategoryUpdateVM model)
		{
			if (id.HasValue && id < 1) return BadRequest();
			if(!ModelState.IsValid) return View(model);
			var entity=await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if(entity is null) return BadRequest();
			entity.Name = model.Name;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
