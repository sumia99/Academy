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

namespace Academy.Controllers
{
    public class TrainersController : Controller
    {
        private readonly AcademyDbContext _context; 
        private IWebHostEnvironment _webHostEnvironment;
        public TrainersController(AcademyDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Trainers
        public async Task<IActionResult> Index()
        {
              return _context.trainers != null ? 
                          View(await _context.trainers.ToListAsync()) :
                          Problem("Entity set 'AcademyDbContext.trainers'  is null.");
        }

        // GET: Trainers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.trainers == null)
            {
                return NotFound();
            }

            var trainer = await _context.trainers
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // GET: Trainers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTrainerViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imgName = FileUpload(model);
                string cv = FileUpload(model);

                Trainer trainer = new Trainer
                {
                    TrainerId = model.TrainerId,
                    TrainerFName = model.TrainerFName,
                    TrainerLName = model.TrainerLName,
                    Email = model.Email,
                    PhoneTraniner = model.PhoneTraniner,
                    BirthDate = model.BirthDate,
                    City = model.City,
                    FB = model.FB,
                    Twitter = model.Twitter,
                    Linkedin = model.Linkedin,
                    TrainerImg=imgName,
                    CVFile = cv,

                    
                   
                };
                _context.trainers.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerId"] = new SelectList(_context.trainers, "CategoryId", "CategoryName", model.TrainerId);
            return View(model);
        }
        public string FileUpload(CreateTrainerViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.TrainerImg!.FileName);
            string newImgName = "Academy" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.TrainerImg.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.TrainerImg.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }
        // GET: Trainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.trainers == null)
            {
                return NotFound();
            }

            var trainer = await _context.trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainerId,TrainerFName,TrainerLName,Email,PhoneTraniner,BirthDate,City,CVFile,TrainerImg,FB,Twitter,Linkedin")] Trainer trainer)
        {
            if (id != trainer.TrainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.TrainerId))
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
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.trainers == null)
            {
                return NotFound();
            }

            var trainer = await _context.trainers
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.trainers == null)
            {
                return Problem("Entity set 'AcademyDbContext.trainers'  is null.");
            }
            var trainer = await _context.trainers.FindAsync(id);
            if (trainer != null)
            {
                _context.trainers.Remove(trainer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerExists(int id)
        {
          return (_context.trainers?.Any(e => e.TrainerId == id)).GetValueOrDefault();
        }
    }
}
