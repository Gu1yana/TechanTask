using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Techan.DataAccessLayer;
using Techan.Models;
using Techan.ViewModels.Sliders;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Slider> datas = [];
            using (var context = new TechanDbContext())
            {
                datas=await context.Sliders.ToListAsync();
            }
            List<SliderGetVm> model = [];
            foreach (var item in datas)
            {
                model.Add(new SliderGetVM()
                { 
                    BigTitle = item.BigTitle;
                    Id=item.Id;
                    ImageUrl=item.ImageUrl;

                })
            }
                return View();

        }
    }
}
