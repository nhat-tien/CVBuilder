using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVBuilder.Data;
using CVBuilder.Models;
using CVBuilder.Utils;
using HandlebarsDotNet;
using Aspose.Html;
using Aspose.Html.Saving;
using Aspose.Html.Rendering.Image;
using Aspose.Html.Converters;
using CVBuilder.Areas.Admin.Dto;

namespace CVBuilder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TemplateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TemplateController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Template
        public async Task<IActionResult> Index()
        {
            var templates = await _context.Templates.Include(t => t.User).ToListAsync();
            //List<Template> templates = [
            //    new Template() {
            //        Id = 1,
            //        Name = "Classic",
            //        PreviewImageUrl = "https://placehold.co/300x400?text=template"
            //    }
            //];
            return View(templates);
        }

        // GET: Admin/Template/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // GET: Admin/Template/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Template/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,PreviewImageUrl,HtmlContent,UserId,PreviewImageBase64")] TemplateCreate templateDto
            )
        {            
            if (ModelState.IsValid)
            {
                var base64 = templateDto.PreviewImageBase64;

                var bytes = Convert.FromBase64String(base64.Replace("data:image/png;base64,", ""));

                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid().ToString() + ".png";
                var filePath = Path.Combine(uploadPath, fileName);

                System.IO.File.WriteAllBytes(filePath, bytes);


                var template = templateDto.ToTemplate();
                template.PreviewImageUrl = "/uploads/" + fileName;

                

                _context.Add(template);
                await _context.SaveChangesAsync();

                TempData["Toasts"] = "[{ title: 'Thành công', content: 'Tạo template thành công', type: 'primary'}]";

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", templateDto.UserId);
            return View(templateDto);
        }

        // GET: Admin/Template/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", template.UserId);

            return View(TemplateCreate.FromTemplate(template));
        }

        // POST: Admin/Template/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,Name,PreviewImageUrl,HtmlContent,UserId,PreviewImageBase64")] TemplateCreate templateDto
        )
        {
            if (id != templateDto.Id)
            {
                return NotFound();
            }

            var template = templateDto.ToTemplate();
            if (ModelState.IsValid)
            {
                try
                {

                var base64 = templateDto.PreviewImageBase64;

                var bytes = Convert.FromBase64String(base64.Replace("data:image/png;base64,", ""));

                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid().ToString() + ".png";
                var filePath = Path.Combine(uploadPath, fileName);

                System.IO.File.WriteAllBytes(filePath, bytes);


                template.PreviewImageUrl = "/uploads/" + fileName;
                    _context.Update(template);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateExists(template.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["Toasts"] = "[{ title: 'Thành công', content: 'Chỉnh sửa template thành công', type: 'primary'}]";

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", template.UserId);

            return View(templateDto);
        }

        // GET: Admin/Template/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: Admin/Template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template != null)
            {
                _context.Templates.Remove(template);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.Id == id);
        }

        private object DefaultData()
        {
            return new
            {
                name = "John Doe",
                role = "Senior Software Engineer",
                contact = new
                {
                    email = "john@example.com",
                    phone = "+1 234 567 890",
                    website = "johndoe.dev",
                    address = "New York, USA"
                },
                skills = new[] {
        "JavaScript / TypeScript",
        "React / Node.js",
        "Microservices Architecture",
        "SQL / NoSQL",
        "Docker / Kubernetes"
    },
                languages = new[] {
        "English (Fluent)",
        "Spanish (Intermediate)"
    },
                profile = "Senior software engineer with 7+ years of experience designing scalable backend systems, leading engineering teams, and delivering high-impact products. Strong background in cloud-native applications and distributed system design.",
                experience = new[] {
        new {
            title = "Lead Software Engineer - ABC Tech",
            date = "2021 - Present",
            bullets = new [] {
                "Designed and built microservice architecture serving 2M+ users.",
                "Led a team of 6 engineers...",
                "Delivered a real-time data pipeline..."
            }
        },
        new {
            title = "Software Engineer - XYZ Corp",
            date = "2018 - 2021",
            bullets = new [] {
                "Developed internal automation tools...",
                "Maintained backend services with 99.98% uptime."
            }
        }
    },
                education = "B.S. in Computer Science - University of Technology (2014 - 2018)"
            };
        }
    }
}
