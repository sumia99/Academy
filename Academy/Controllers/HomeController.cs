using Academy.Data;
using Academy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Academy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AcademyDbContext db;

        public HomeController(ILogger<HomeController> logger, AcademyDbContext _db)
        {
            _logger = logger;
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TrainerReg()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TrainerReg(Trainer T)
        { if (ModelState.IsValid)
            {
                await db.trainers.AddAsync(T);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            return View();
        }
        [HttpGet]
        public IActionResult OrderCourse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OrderCourse(Student s , int id)
        {
           
            var Course = db.courses.Include(e => e.CourseId);
            if (ModelState.IsValid)
            {
                s.c!.Add((Course)Course);
                await db.SaveChangesAsync();
              
            }

            return View();
        }
        public IActionResult CourseByCate(int id) { 
            var result = db.courses.Where( e => e.CategoryId ==id).OrderByDescending(v =>v.StartDate).ToList();
        return View(result);
        }
        public IActionResult  TrainerForm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TrainerForm(Trainer s)
        {
            if (ModelState.IsValid)
            {
                await db.trainers.AddAsync(s);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            return View();
        }
        public IActionResult SearchB(string c)
        {

            return View(db.courses.Where(a => a.CourseTitle!.Contains(c)).ToList());
        }
        public IActionResult CourseDescription(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = db.courses.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}