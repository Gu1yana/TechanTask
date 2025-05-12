using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Brands;
using Techan.ViewModels.Products;
using Techan.ViewModels.Sliders;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(TechanDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var brands=await _context.Brands.Select(
                x=>new BrandGetVM
                {
                    Id= x.Id,
                    Name= x.Name,
                }).ToListAsync();
            ViewBag.Brands = brands;
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

    }
}