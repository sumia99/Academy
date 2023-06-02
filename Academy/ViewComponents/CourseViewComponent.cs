using Academy.Data;
using Microsoft.AspNetCore.Mvc;

namespace Academy.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private AcademyDbContext db;
        public CourseViewComponent(AcademyDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.courses.OrderByDescending(x => x.CourseId).
                Take(6);
            return View(data);
        }

    }
}
