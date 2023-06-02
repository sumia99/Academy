using Academy.Data;
using Microsoft.AspNetCore.Mvc;

namespace Academy.ViewComponents
{
    public class ContactUsViewComponent : ViewComponent
    {
        private AcademyDbContext db;
        public ContactUsViewComponent(AcademyDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
           
            return View();
        }
    }
}
