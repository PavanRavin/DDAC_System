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
    [Authorize(Roles = "Teacher,Student")]
    public class AssignmentController : Controller
        //private readonly System_ClassDBContext _ClassDBContext;
        private readonly UserManager<DDAC_SystemUser> _userManager;
        private readonly System_ClassDBContext _context;

        public AssignmentController(System_ClassDBContext context, UserManager<DDAC_SystemUser> usermanager)
        {
            _userManager = usermanager;
        }

        public SelectList GetTeacherClass()
        {
            var teacherClass = _context.AcademicClass
                .Where(e => e.Class_Lecturer == User.Identity.Name)
                .Select(e => e.Class_Name)
                .ToList();
            SelectList classes = new SelectList(teacherClass, "Class_Name");
            return classes;
        }

    public async Task<IActionResult> Index()
        {
            return View(await _context.Assignment.ToListAsync());
        }

        // GET: Assignment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.AssignmentID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignment/Create
        [Authorize(Roles ="Teacher")]
        public IActionResult Create()
        {
            ViewBag.ClassNameList = GetTeacherClass();
            return View();
        }

        // POST: Assignment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create([Bind("AssignmentID,AssignmentName,AssignmentDesc,AssignmentHandOut,AssignmentDue,Class_Name")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assignment);
        }

        // GET: Assignment/Edit/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewBag.ClassNameList = GetTeacherClass();
            return View(assignment);
        }

        // POST: Assignment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentID,AssignmentName,AssignmentDesc,AssignmentHandOut,AssignmentDue,Class_Name")] Assignment assignment)
        {
            if (id != assignment.AssignmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.AssignmentID))
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
            return View(assignment);
        }

        // GET: Assignment/Delete/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.AssignmentID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignment.FindAsync(id);
            _context.Assignment.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SubmitAssignment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .FirstOrDefaultAsync(m => m.AssignmentID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "AssignmentSubmission", assignment);
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignment.Any(e => e.AssignmentID == id);
        }
    }
}

