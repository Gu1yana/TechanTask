using Microsoft.AspNetCore.Mvc;

namespace Techan.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
