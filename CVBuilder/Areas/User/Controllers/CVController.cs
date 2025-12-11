using CVBuilder.Areas.User.ViewModels.CV;
using CVBuilder.Data;
using CVBuilder.Models;
using HandlebarsDotNet.Runtime;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBuilder.Areas.User.Controllers
{
    [Area("User")]
    public class CVController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager; 

        public CVController(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User/CV
        public async Task<IActionResult> Index(string? q)
        {
            var cvs = await _context.CV.Include(c => c.Profile)
                .Include(c => c.Template)
                .Include(c => c.User)
                 .Where(cv =>
                   (q == null) ||
                       cv.Title.Contains(q))
                .ToListAsync();


            var templates = await _context.Templates.Include(t => t.User).ToListAsync();

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

            var profile = cV.Profile;

            var cvReq = new NewCVRequest()
            {
                TemplateId = cV.Template.Id,
                FileName = cV.FileName,
                Title = cV.Title,
                ThemeColor = cV.ThemeColor,
                FullName = profile.FullName,
                Email = profile.Email,
                Phone = profile.Phone,
                Address = profile.Address,
                Sections = profile.ProfileSections,
            };

            return View(cvReq);
        }

        // GET: User/CV/Create
        public async Task<IActionResult> Create(int templateId)
        {
            
            var user = await _userManager.GetUserAsync(User);

            var template = await _context.Templates
                .Where(t => t.Id == templateId)
                .FirstOrDefaultAsync();

            ViewBag.Template = template;
            ViewBag.TemplateId = templateId;

            return View();

        }

        // POST: User/CV/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewCVRequest req)
        {
            if (ModelState.IsValid)
            {
                var templateExists = await _context.Templates
                    .AnyAsync(t => t.Id == req.TemplateId);

                if (!templateExists)
                {
                    TempData["Toasts"] = "[{ title: 'Lỗi', content: 'Không tìm thấy template', type: 'primary'}]";
                    return View(req);
                }

                var userId = _userManager.GetUserId(User)!;

                var profile = new Profile
                {
                    UserId = userId,
                    FullName = req.FullName,
                    Title = req.Title,
                    Summary = "",
                    Email = req.Email,
                    Phone = req.Phone,
                    Address = req.Address,
                    ProfileSections = req.Sections ?? "",
                    IsDefault = false
                };

                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();

                var cv = new CV
                {
                    TemplateId = req.TemplateId,
                    FileName = req.FileName,
                    Title = req.Title,
                    ThemeColor = req.ThemeColor,
                    ProfileId = profile.Id,
                    UserId = userId 
                };

                _context.CV.Add(cv);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(req);
        }

        // GET: User/CV/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            var profile = cV.Profile;

            ViewBag.Template = cV.Template;
            ViewBag.TemplateId = cV.TemplateId;

            var cvReq = new NewCVRequest()
            {
                Id = cV.Id,
                TemplateId = cV.Template.Id,
                FileName = cV.FileName,
                Title = cV.Title,
                ThemeColor = cV.ThemeColor,
                FullName = profile.FullName,
                Email = profile.Email,
                Phone = profile.Phone,
                Address = profile.Address,
                Sections = profile.ProfileSections,
            };

            return View(cvReq);
        }

        // POST: User/CV/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewCVRequest req)
        {
            if (id != req.Id)
            {
                return NotFound();
            }

                    var cv = await _context.CV
        .Include(c => c.Profile)
        .FirstOrDefaultAsync(c => c.Id == id);

                    if (cv == null)
                        return NotFound();

            if (ModelState.IsValid)
            {
                try
                {


                    cv.Profile.FullName = req.FullName;
                    cv.Profile.Title = req.Title;
                    cv.Profile.Email = req.Email;
                    cv.Profile.Phone = req.Phone;
                    cv.Profile.Address = req.Address;
                    cv.Profile.ProfileSections = req.Sections ?? "";

                    cv.FileName = req.FileName;
                    cv.Title = req.Title;
                    cv.ThemeColor = req.ThemeColor;
                    cv.TemplateId = req.TemplateId;

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CVExists(cv.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewBag.Template = cv.Template;
            ViewBag.TemplateId = cv.TemplateId;
            return View(req);
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
