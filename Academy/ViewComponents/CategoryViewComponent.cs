using Academy.Data;
using Microsoft.AspNetCore.Mvc;

namespace Academy.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private AcademyDbContext db;
        public CategoryViewComponent(AcademyDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.categories.OrderByDescending(x => x.CategoryId).
                Take(6);
            return View(data);
        }

    }
}
