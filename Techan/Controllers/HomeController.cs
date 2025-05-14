using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Techan.DataAccessLayer;
using Techan.HomeViewModels;
using Techan.ViewModels.Brands;
using Techan.ViewModels.Products;
using Techan.ViewModels.Sliders;

namespace Techan.Controllers
{
    public class HomeController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {

			var sliders = await _context.Sliders.Select(
					x => new SliderGetVM
					{
						Id = x.Id,
						Title = x.Title,
						LittleTitle= x.LittleTitle,
						BigTitle= x.BigTitle,
						Offer= x.Offer,
						ImagePath= x.ImagePath,
						Link= x.Link,
					}).ToListAsync();
			var products = await _context.Products.Select(
				x => new ProductListVM
				{
					Id= x.Id,
                    Name= x.Name,
                    Description= x.Description,
                    Discount= x.Discount,
                    SellPrice= x.SellPrice,
                    ImageUrl= x.ImageUrl,
					CategoryName=x.Category.Name,
					BrandName=x.Brand.Name,

                }).ToListAsync();
			HomeVM homeVM = new (){
				Sliders = sliders,
				Products = products,
			};
			return View(homeVM);
        }
    }
}
