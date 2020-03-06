using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Database;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        // GET: Students
        public ViewResult Index(string sortOrder, string searchString)
        {
            var studentContext = _context.Student.ToList();


            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FatherNameSortParm = sortOrder == "fname_asc" ? "fname_desc" : "fname_asc";
            ViewBag.Gender = sortOrder == "gender_asc" ? "gender_desc" : "gender_asc";
            ViewBag.Address = sortOrder == "addressname_asc" ? "addressname_desc" : "addressname_asc";
            ViewBag.Email = sortOrder == "emailname_asc" ? "emailname_desc" : "emailname_asc";
            ViewBag.PhoneNo = sortOrder == "phonenoname_asc" ? "phonenoname_desc" : "phonenoname_asc";
            ViewBag.DOB = sortOrder == "dob_asc" ? "dob_desc" : "dob_asc";

            var students = from s in studentContext
                           select s;


            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.FatherName.Contains(searchString)
                                       || s.Address.Contains(searchString)
                                       || s.Gender.Contains(searchString)
                                       || s.PhoneNo.ToString().Contains(searchString)
                                       || s.DOB.ToString().Contains(searchString)
                                       || s.Email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "fname_desc":
                    students = students.OrderByDescending(s => s.FatherName);
                    break;
                case "fname_asc":
                    students = students.OrderBy(s => s.FatherName);
                    break;
                case "gender_desc":
                    students = students.OrderByDescending(s => s.Gender);
                    break;
                case "gender_asc":
                    students = students.OrderBy(s => s.Gender);
                    break;
                case "addressname_desc":
                    students = students.OrderByDescending(s => s.Address);
                    break;
                case "addressname_asc":
                    students = students.OrderBy(s => s.Address);
                    break;
                case "emailname_desc":
                    students = students.OrderByDescending(s => s.Email);
                    break;
                case "emailname_asc":
                    students = students.OrderBy(s => s.Email);
                    break;
                case "phonenoname_desc":
                    students = students.OrderByDescending(s => s.PhoneNo);
                    break;
                case "phonenoname_asc":
                    students = students.OrderBy(s => s.PhoneNo);
                    break;
                case "dob_desc":
                    students = students.OrderByDescending(s => s.DOB);
                    break;
                case "dob_asc":
                    students = students.OrderBy(s => s.DOB);
                    break;
       

                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,FatherName,Gender,Address,Email,PhoneNo,DOB,Id")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,FatherName,Gender,Address,Email,PhoneNo,DOB,Id")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}
