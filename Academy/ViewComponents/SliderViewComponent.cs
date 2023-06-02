using Academy.Data;
using Microsoft.AspNetCore.Mvc;

namespace Academy.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private AcademyDbContext db;
        public SliderViewComponent(AcademyDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.sliders.OrderByDescending(x => x.SliderId).
                Take(3);
            return View(data);
        }

    }
   
}
