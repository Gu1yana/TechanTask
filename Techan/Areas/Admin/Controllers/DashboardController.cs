using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Techan.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles= "Superadmin, Admin,Moderator")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}