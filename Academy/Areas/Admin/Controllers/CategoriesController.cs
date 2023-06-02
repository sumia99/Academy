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

namespace Academy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly AcademyDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(AcademyDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
              return _context.categories != null ? 
                          View(await _context.categories.ToListAsync()) :
                          Problem("Entity set 'AcademyDbContext.categories'  is null.");
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            var category = await _context.categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imgN = FileUpload(model);
                Category category = new Category
                {
                   CategoryId = model.CategoryId,
                   CategoryName = model.CategoryName,

                    Image = imgN
                };
                _context.categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }
             var category = await _context.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName", category.CategoryId);
            CategoryEditViewModel editViewModel = new CategoryEditViewModel
            {   CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
             };
            return View(editViewModel);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryEditViewModel model)
        {
            if (id != model.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            { 
                try
                {
                    string imgName;
                    if (model.Image == null)
                    {
                        var x = _context.categories.AsNoTracking().Where(x => x.CategoryId == model.CategoryId).FirstOrDefault();
                        imgName = x.Image;
                    }
                    else
                    {
                        imgName = FileUpload(model);
                    }
                    Category category = new Category
                    {
                        CategoryId = model.CategoryId,
                        CategoryName = model.CategoryName,

                        Image = imgName


                    };
                    _context.categories.Update(category);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(model.CategoryId))
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
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CategoryName",model.CategoryId);
            return View(model);
        }
        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            var category = await _context.categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.categories == null)
            {
                return Problem("Entity set 'AcademyDbContext.categories'  is null.");
            }
            var category = await _context.categories.FindAsync(id);
            if (category != null)
            {
                _context.categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
        public string FileUpload(CategoryViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.Image!.FileName);
            string newImgName = "Academy" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Image.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }
        public string FileUpload(CategoryEditViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.Image!.FileName);
            string newImgName = "cat" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Image.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }

    }
}
