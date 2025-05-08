using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Sliders;
using Techan.Models.Common;

namespace Techan.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SliderController(TechanDbContext _context) : Controller
	{
		public async Task<IActionResult> Index()
		{
            List<Slider> datas = [];
			datas = await _context.Sliders.ToListAsync();
                List<SliderGetVM> vms = [];
                foreach (var data in datas)
                {
                    vms.Add(new SliderGetVM
                    {
                        Id = data.Id,
                        Offer = data.Offer,
                        Title = data.Title,
                        BigTitle = data.BigTitle,
                        LittleTitle = data.LittleTitle,
                        ImagePath= data.ImagePath,
                        Link = data.Link,
                    });
                }
                return View(vms);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SliderCreateVM model)
        {
            if(!ModelState.IsValid)
                return View(model);
            string newName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile!.FileName);
            string path=Path.Combine("imgs","sliders",newName);
            await using (FileStream fs = System.IO.File.Create(newName))
            {
                await model.ImageFile.CopyToAsync(fs);
            }
            Slider slider = new()
            {
                BigTitle = model.BigTitle,
                LittleTitle = model.LittleTitle,
                Title = model.Title,
                Offer = model.Offer,
                Link = model.Link,
                ImagePath = newName
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(!id.HasValue || id.Value<1 ) return BadRequest();
            int result = await _context.Sliders.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == 0)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id.HasValue && id < 1) return BadRequest();
            var entity=await _context.Sliders.Select(x => new SliderUpdateVM
            {
                Id=x.Id,
                Offer = x.Offer,
                Link = x.Link,
                ImagePath = x.ImagePath,
                Title = x.Title,
                BigTitle = x.BigTitle,
                LittleTitle = x.LittleTitle
            }).FirstOrDefaultAsync(x => x.Id == id);
            if(entity is null) return NotFound();
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM model)
        {
            if(id.HasValue && id<1) return BadRequest();
            if (!ModelState.IsValid) return View(model);
           var entity=await _context.Sliders.FirstOrDefaultAsync(x=>x.Id==id);
            if (entity is null) return BadRequest();
            if(model.ImagePath!=null)
            {
				string newFileName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile.FileName);
                string newPath = Path.Combine("wwwroot", "imgs", "brands", newFileName);
                await using(FileStream fs = System.IO.File.Create(newPath))
                {
                   await model.ImageFile.CopyToAsync(fs);
                }
                entity.ImagePath = newFileName;
                entity.Title = model.Title; 
                entity.BigTitle = model.BigTitle;
                entity.Offer = model.Offer;
                entity.LittleTitle = model.LittleTitle;
                entity.Link = model.Link;
            }
             await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
