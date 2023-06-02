using Academy.Data;
using Microsoft.AspNetCore.Mvc;

namespace Academy.ViewComponents
{
    public class AboutUsViewComponent : ViewComponent
    {
        private AcademyDbContext db;
        public AboutUsViewComponent(AcademyDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
