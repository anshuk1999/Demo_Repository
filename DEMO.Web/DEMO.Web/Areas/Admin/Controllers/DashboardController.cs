using DEMO.Web.Areas.Admin.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Web.Areas.Admin.Controllers
{
    [SessionAuth]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}