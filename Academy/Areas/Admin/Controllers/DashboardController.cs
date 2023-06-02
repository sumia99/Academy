using Academy.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academy.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {

        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
