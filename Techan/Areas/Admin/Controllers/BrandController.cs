using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Brands;

namespace Techan.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "User,Superadmin,Admin,Moderator")]
    public class BrandController(TechanDbContext _context) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var datas = await _context.Brands.Select(x => new BrandGetVM
			{
				Id = x.Id,
				Name = x.Name,
				ImageUrl = x.ImageUrl,
            }).ToListAsync();
			return View(datas);
		}
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BrandCreateVM model)
		{
			if (model.ImageFile != null)
			{
				if (!model.ImageFile.ContentType.StartsWith("image"))
				{
					string ext = Path.GetExtension(model.ImageFile.FileName).ToLower();
					ModelState.AddModelError("ImageFile", "Fayl shekil formatinda olmalidir" + ext + "olmaz!");
				}
				if (model.ImageFile.Length / 1024 > 200)
					ModelState.AddModelError("ImageFile", "Shekilin olcusu 200 Kb-dan artiq ola bilmez!");
			}
			if (!ModelState.IsValid) return View(model);
			string newName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile!.FileName);
			string path = Path.Combine("wwwroot", "imgs", "brands", newName);
			await using (FileStream fs = System.IO.File.Create(path))
			{
				await model.ImageFile.CopyToAsync(fs);
			}
			Brand brand = new()
			{
				ImageUrl = newName,
				Name = model.Name,
			};
			await _context.Brands.AddAsync(brand);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (!id.HasValue || id < 1) return BadRequest();
			int result = await _context.Brands.Where(x => x.Id == id).ExecuteDeleteAsync();
			if (result == 0)
				return NotFound();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Update(int? id)
		{
			
			if (!id.HasValue || id < 1) return BadRequest();
			var brand = await _context.Brands
				.Where(x=>x.Id==id)
				.Select(x => new BrandUpdateVM
			{
				Name = x.Name,
				ImageUrl = x.ImageUrl,
			}).FirstOrDefaultAsync();
			if (brand is null) return NotFound();
			return View(brand);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(int? id, BrandUpdateVM model)
		{

			if (!id.HasValue || id < 1) return BadRequest(); 
			if (model.ImageFile!=null)
			{
				if (!model.ImageFile.ContentType.StartsWith("image"))
				{
					string ext = Path.GetExtension(model.ImageFile.FileName).ToLower();
					ModelState.AddModelError("ImageFile", "Fayl shekil formatinda olmalidir" + ext + "olmaz!");
				}
				if (model.ImageFile.Length / 1024 > 200)
				ModelState.AddModelError("ImageFile", "Shekilin olcusu 200 Kb-dan artiq ola bilmez!");
						
			}
			if (!ModelState.IsValid)
				return View(model);
			var brand = await _context.Brands.FindAsync(id);
			if(brand is null) return NotFound();
			if(model.ImageFile!=null)
			{
				string path = Path.Combine("wwwroot", "imgs", "brands", brand.ImageUrl);
				await using (FileStream fs = new FileStream(path, FileMode.Create))
				{
					await model.ImageFile.CopyToAsync(fs);
				}
			}
			brand.Name = model.Name;
			await  _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}