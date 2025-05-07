using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Products;
using Techan.ViewModels.Sliders;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Product> datas = [];
            datas = await _context.Products.ToListAsync();
            List<ProductGetVM> vms = [];
            foreach (var data in datas)
            {
                vms.Add(new ProductGetVM
                {
                    Id = data.Id,
                    Name = data.Name,
                    Description = data.Description,
                    Discount = data.Discount,
                    Price = data.Price,
                    ImageUrl = data.ImageUrl,
                    BrandId = data.BrandId,
                    CategoryId = data.CategoryId,
                });
            }
            return View(vms);
        }
    }
}
