using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Academy.Data;
using Academy.Models;
using Academy.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Academy.ViewComponents;

namespace Academy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly AcademyDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        public CoursesController(AcademyDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: Admin/Courses
        public async Task<IActionResult> Index()
        {
            var academyDbContext = _context.courses.Include(c => c.Category);
            return View(await academyDbContext.ToListAsync());
        }

        // GET: Admin/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Admin/Courses/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imgName = FileUpload(model);
                Course course = new Course
                {
                    CategoryId = model.CategoryId,
                    CourseDesc = model.CourseDesc,
                    CourseTitle = model.CourseTitle,
                   
                    Duration = model.Duration,
                    Hours = model.Hours,
                   Learn = model.Learn,
                   certificate =model.certificate,
                    Price = model.Price,
                    Rate = model.Rate,
                    StartDate = model.StartDate,
                    StartTime = model.StartTime,
                 
                  
                    Img = imgName
                };
                _context.courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }
        
        // GET: Admin/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var model = await _context.courses.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName", model.CategoryId);
            CourseEditViewModel courseView = new CourseEditViewModel
            {
                CategoryId = model.CategoryId,
                CourseDesc = model.CourseDesc,
                CourseTitle = model.CourseTitle,
                certificate = model.certificate,
                Duration = model.Duration,
                Hours = model.Hours,
                Learn = model.Learn,
                Price = model.Price,
                Rate = model.Rate,
                StartDate = model.StartDate,
                StartTime = model.StartTime,


            };
            return View(courseView);
        }
        public IActionResult SearchBox(string c)
        {

            return View(_context.courses.Where(a => a.CourseTitle!.Contains(c)).ToList());
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseEditViewModel model) {

            if (id != model.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    string imgName;
                    if (model.Img == null)
                    {
                        var x = _context.courses.AsNoTracking().Where(x => x.CourseId == model.CourseId).FirstOrDefault();
                        imgName = x.Img;
                    }
                    else
                    {
                        imgName = FileUpload(model);
                    }
                    Course course = new Course
                    {
                        CategoryId = model.CategoryId,
                        CourseDesc = model.CourseDesc,
                        CourseTitle = model.CourseTitle,
                        certificate = model.certificate,
                        Duration = model.Duration,
                        Hours = model.Hours,
                        Learn = model.Learn,
                        Price = model.Price,
                        Rate = model.Rate,
                        StartDate = model.StartDate,
                        StartTime = model.StartTime,
                        Img=imgName
                    };
                    _context.courses.Update(course);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(model.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }
        // GET: Admin/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        public IActionResult SearchB (string s)
        {
                return View(_context.courses.Where(a => a.CourseTitle!.Contains(s)).ToList());
        }
       
        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.courses == null)
            {
                return Problem("Entity set 'AcademyDbContext.courses'  is null.");
            }
            var course = await _context.courses.FindAsync(id);
            if (course != null)
            {
                _context.courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public string FileUpload(CourseViewModel model)
        {
            string w = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(w)) { }
            string contentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentPath)) { }
            string p = Path.Combine(w, "Images");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(model.Img!.FileName);
            string newImgName = "Academy" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.Img.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Img.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }

        public string FileUpload(CourseEditViewModel model)
        {
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { }
            string contentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentPath)) { }
            string p = Path.Combine(wwwPath, "Images");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(model.Img!.FileName);
            string newImgName = "cat" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.Img.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Img.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }

        private bool CourseExists(int id)
        {
          return (_context.courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
