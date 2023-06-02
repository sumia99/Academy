using Academy.Data;
using Microsoft.AspNetCore.Mvc;

namespace Academy.ViewComponents
{
    public class TrainerViewComponent : ViewComponent
    {
        private AcademyDbContext db;
        public TrainerViewComponent(AcademyDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.trainers.OrderByDescending(x => x.TrainerId).
                Take(6);
            return View(data);
        }

    }
}
