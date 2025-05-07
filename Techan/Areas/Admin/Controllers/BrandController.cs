using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Brands;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Brands.Select(x => new BrandGetVM
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl
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
            if (!ModelState.IsValid) return View(model);
            string newName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile!.FileName);
            string path = Path.Combine("imgs", "brands", newName);
            await using (FileStream fs = System.IO.File.Create(newName))
            {
                await model.ImageFile.CopyToAsync(fs);
            }
            Brand brand = new()
            {
                Name=model.Name,
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
            if (id.HasValue || id < 1) return BadRequest();
            var entity=await _context.Brands.Select(x => new BrandUpdateVM
            {
                Name=x.Name,
                ImageUrl=x.ImageUrl,
            }).FirstOrDefaultAsync(x=>x.Id==id);
            if(entity == null) return NotFound();
            return View(entity);
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, BrandUpdateVM model)
        {
            if (id.HasValue || id < 1) return BadRequest();
            if(!ModelState.IsValid) return View(model);
            var entity=await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return BadRequest();
            if(model.ImageUrl!=null)
            {
				string newFileName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile.FileName);
				string newPath = Path.Combine("wwwroot", "imgs", "brands", newFileName);
				await using (FileStream fs = System.IO.File.Create(newPath))
				{
					model.ImageFile.CopyToAsync(fs);
				}
				entity.Name = model.Name;
                entity.ImageUrl =newFileName;
			}
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
        }
    }
}
