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

namespace Academy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainersController : Controller
    {
        private readonly AcademyDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public TrainersController(AcademyDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;   
        }

        // GET: Admin/Trainers
        public async Task<IActionResult> Index()
        {
              return _context.trainers != null ? 
                          View(await _context.trainers.ToListAsync()) :
                          Problem("Entity set 'AcademyDbContext.trainers'  is null.");
        }

        // GET: Admin/Trainers/Details/5
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

        // GET: Admin/Trainers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreateTrainerViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imgName = FileUpload(model);
                string CVName = FileUploadcv(model);
                Trainer t = new Trainer
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
                    TrainerImg= imgName,
                    CVFile = CVName,
                };
                _context.trainers.Add(t);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerId"] = new SelectList(_context.trainers, "CategoryId", "CategoryName", model.TrainerId);
            return View(model);
        }

        // GET: Admin/Trainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.trainers == null)
            {
                return NotFound();
            }

            var T = await _context.trainers.FindAsync(id);
            if (T == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.trainers, "TrainerId", "TrainerName", T.TrainerId);
            CreateTrainerViewModel trainer = new CreateTrainerViewModel
            {
                TrainerId = T.TrainerId,
                TrainerFName = T.TrainerFName,
                TrainerLName = T.TrainerLName,
                Email = T.Email,
                PhoneTraniner = T.PhoneTraniner,
                BirthDate = T.BirthDate,
                City = T.City,
                FB = T.FB,
                Twitter = T.Twitter,
                Linkedin = T.Linkedin,
            };
            return View(trainer);
        }

        // POST: Admin/Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateTrainerViewModel model) {

            if (id != model.TrainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    string imgName;
                    string CVName = FileUploadcv(model);
                    if (model.TrainerImg == null || model.CVFile==null)
                    {
                        var x = _context.trainers.AsNoTracking().Where(x => x.TrainerId == model.TrainerId).FirstOrDefault();
                        imgName = x.TrainerImg;
                        CVName = x.CVFile;  
                    }
                    else
                    {
                        imgName = FileUpload(model);
                        CVName = FileUploadcv(model);

                    }
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
                        TrainerImg = imgName,
                        CVFile = CVName,


                    };
                    _context.trainers.Update(trainer);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(model.TrainerId))
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
            ViewData["CategoryId"] = new SelectList(_context.trainers, "TrainerId", "TrainerName", model.TrainerId);
            return View(model);


        }

        // GET: Admin/Trainers/Delete/5
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

        // POST: Admin/Trainers/Delete/5
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
        public string FileUpload(CreateTrainerViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.TrainerImg!.FileName);
            string newImgName = "Trainer" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.TrainerImg.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.TrainerImg.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }
        public string FileUploadcv(CreateTrainerViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.CVFile!.FileName);
            string NewFile = "Trainer" + fileName + "_" +
                Guid.NewGuid().ToString() + Path.GetExtension(model.CVFile.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, NewFile), FileMode.Create))
            {
                model.CVFile.CopyTo(fileStream);
            }
            return "\\Images\\" + NewFile;
        }
        private bool TrainerExists(int id)
        {
          return (_context.trainers?.Any(e => e.TrainerId == id)).GetValueOrDefault();
        }
    }
}
