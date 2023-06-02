using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Academy.Data;
using Academy.Models;

namespace Academy.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly AcademyDbContext _context;

        public ContactUsController(AcademyDbContext context)
        {
            _context = context;
        }

        // GET: ContactUs
        public async Task<IActionResult> Index()
        {
              return _context.contactUs != null ? 
                          View(await _context.contactUs.ToListAsync()) :
                          Problem("Entity set 'AcademyDbContext.contactUs'  is null.");
        }

        // GET: ContactUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.contactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.contactUs
                .FirstOrDefaultAsync(m => m.ContactUsId == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactUsId,Name,Email,Phone,Location,Massage,Description,Address")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactUs);
        }

        // GET: ContactUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.contactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.contactUs.FindAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }
            return View(contactUs);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactUsId,Name,Email,Phone,Location,Massage,Description,Address")] ContactUs contactUs)
        {
            if (id != contactUs.ContactUsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsExists(contactUs.ContactUsId))
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
            return View(contactUs);
        }

        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.contactUs == null)
            {
                return NotFound();
            }

            var contactUs = await _context.contactUs
                .FirstOrDefaultAsync(m => m.ContactUsId == id);
            if (contactUs == null)
            {
                return NotFound();
            }

            return View(contactUs);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.contactUs == null)
            {
                return Problem("Entity set 'AcademyDbContext.contactUs'  is null.");
            }
            var contactUs = await _context.contactUs.FindAsync(id);
            if (contactUs != null)
            {
                _context.contactUs.Remove(contactUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUsExists(int id)
        {
          return (_context.contactUs?.Any(e => e.ContactUsId == id)).GetValueOrDefault();
        }
    }
}
