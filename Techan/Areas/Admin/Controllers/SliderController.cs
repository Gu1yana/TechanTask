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
			List<SliderGetVM> sliders = [];
			foreach (var item in datas)
			{
				sliders.Add(new SliderGetVM()
				{
					BigTitle = item.BigTitle,
					Id = item.Id,
					ImageUrl = item.ImageUrl,
					Link = item.Link,
					LittleTitle = item.LittleTitle,
					Offer = item.Offer,
					Title = item.Title
				});
			}
			return View(sliders);
		}
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SliderCreateVM model)
		{
			if (!ModelState.IsValid)
				return View(model);
			Slider slider = new();
			slider.LittleTitle = model.LittleTitle;
			slider.Title = model.Title;
			slider.BigTitle = model.BigTitle;
			slider.Offer = model.Offer;
			slider.Link = model.Link;
			slider.ImageUrl = model.ImageUrl;
			
			await _context.Sliders.AddAsync(slider);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int?id)
		{
			int result = await _context.Sliders.Where(x => x.Id == id).ExecuteDeleteAsync();
				if (result == 0)
					return NotFound();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Update(int? id)
		{
			if (id.HasValue && id < 1) return BadRequest();
			var entity=await _context.Sliders.Select(x=>new SliderUpdateVM
			{
				Id=x.Id,
				Title=x.Title,
				BigTitle=x.BigTitle,
				Offer=x.Offer,
				Link=x.Link,
				ImageUrl=x.ImageUrl,
				LittleTitle=x.LittleTitle,
			}).FirstOrDefaultAsync(x=>x.Id==id);
			if(entity==null) return NotFound();	
			return View(entity);
		}
		[HttpPost]
		public async Task<IActionResult> Update (int? id, SliderUpdateVM vm)
		{
			if(id.HasValue && id<1)
				return BadRequest();
			if(!ModelState.IsValid)
				return View(vm);
			var entity = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
			if(entity==null) return BadRequest();
			entity.BigTitle = vm.BigTitle;
			entity.Offer = vm.Offer;
			entity.Link = vm.Link;
			entity.ImageUrl = vm.ImageUrl;
			entity.LittleTitle = vm.LittleTitle;
			entity.Title = vm.Title;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
