using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDAC_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace DDAC_System.Controllers
{
    [Authorize(Roles="Admin,Teacher,Student")]
    public class AcademicClassController : Controller
    {
        private readonly System_ClassDBContext _context;

        public AcademicClassController(System_ClassDBContext context)
        {
            _context = context;
        }

        // GET: AcademicClass
        public async Task<IActionResult> Index()
        {
            return View(await _context.AcademicClass.ToListAsync());
        }

        // GET: AcademicClass/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicClass = await _context.AcademicClass
                .FirstOrDefaultAsync(m => m.Class_ID == id);
            if (academicClass == null)
            {
                return NotFound();
            }

            return View(academicClass);
        }

        // GET: AcademicClass/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcademicClass/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Class_ID,Class_Name,Class_Lecturer,ClassStartTime,ClassEndTime,Class_Location")] AcademicClass academicClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academicClass);
        }

        // GET: AcademicClass/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicClass = await _context.AcademicClass.FindAsync(id);
            if (academicClass == null)
            {
                return NotFound();
            }
            return View(academicClass);
        }

        // POST: AcademicClass/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Class_ID,Class_Name,Class_Lecturer,ClassStartTime,ClassEndTime,Class_Location")] AcademicClass academicClass)
        {
            if (id != academicClass.Class_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicClassExists(academicClass.Class_ID))
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
            return View(academicClass);
        }

        // GET: AcademicClass/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicClass = await _context.AcademicClass
                .FirstOrDefaultAsync(m => m.Class_ID == id);
            if (academicClass == null)
            {
                return NotFound();
            }

            return View(academicClass);
        }

        // POST: AcademicClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicClass = await _context.AcademicClass.FindAsync(id);
            _context.AcademicClass.Remove(academicClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicClassExists(int id)
        {
            return _context.AcademicClass.Any(e => e.Class_ID == id);
        }
    }
}
