using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVBuilder.Data;
using CVBuilder.Models;
using CVBuilder.Areas.User.ViewModels.CV;

namespace CVBuilder.Areas.User.Controllers
{
    [Area("User")]
    public class CVController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CVController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User/CV
        public async Task<IActionResult> Index()
        {
            // var cvs = await _context.CV.Include(c => c.Profile).Include(c => c.Template).Include(c => c.User).ToListAsync();
            List<CV> cvs = [
                new CV() {
                    Id = 1,
                    TemplateId = 1,
                    ProfileId = 1,
                    Title = "New CV",
                    UpdatedAt = DateTime.Now.AddHours(-2)
                }
            ];

            List<Template> templates = [
                new Template() {
                    Id = 1,
                    Name = "Classic",
                    PreviewImageUrl = "https://placehold.co/300x400?text=template"
                }
            ];

            var model = new CVIndexViewModel() {
                CVs = cvs,
                Templates = templates
            };

            return View(model);
        }

        // GET: User/CV/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV
                .Include(c => c.Profile)
                .Include(c => c.Template)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // GET: User/CV/Create
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: User/CV/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProfileId,TemplateId,FileName,ThemeColor,CreatedAt,UpdatedAt")] CV cV)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", cV.ProfileId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Name", cV.TemplateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cV.UserId);
            return View(cV);
        }

        // GET: User/CV/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV.FindAsync(id);
            if (cV == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", cV.ProfileId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Name", cV.TemplateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cV.UserId);
            return View(cV);
        }

        // POST: User/CV/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProfileId,TemplateId,FileName,ThemeColor,CreatedAt,UpdatedAt")] CV cV)
        {
            if (id != cV.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CVExists(cV.Id))
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", cV.ProfileId);
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Name", cV.TemplateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cV.UserId);
            return View(cV);
        }

        // GET: User/CV/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CV
                .Include(c => c.Profile)
                .Include(c => c.Template)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // POST: User/CV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cV = await _context.CV.FindAsync(id);
            if (cV != null)
            {
                _context.CV.Remove(cV);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CVExists(int id)
        {
            return _context.CV.Any(e => e.Id == id);
        }
    }
}
