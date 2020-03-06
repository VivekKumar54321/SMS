using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Database;
using StudentManagement.Models;
using StudentManagement.ViewModel;

namespace StudentManagement.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly StudentContext _context;

        public RegistrationsController(StudentContext context)
        {
            _context = context;
        }

        // GET: Registrations
        public ViewResult  Index(string sortOrder, string searchString)
        {


            var studentContext = _context.Registration.Include(r => r.Faculty).Include(r => r.Payment).Include(r => r.Student);
   


                 ViewBag.CurrentSort = sortOrder;
                 ViewBag.StudentName = string.IsNullOrEmpty(sortOrder) ? "studentname_desc" : "";
                 ViewBag.StudentAddress = sortOrder == "addressname_asc" ? "addressname_desc" : "addressname_asc";
                  ViewBag.StudentPhoneNo = sortOrder == "phonenoname_asc" ? "phonenoname_desc" : "phonenoname_asc";
                 ViewBag.PaymentType = sortOrder == "type_asc" ? "type_desc" : "type_asc";
                 ViewBag.PaymentAmount = sortOrder == "amt_asc" ? "amt_desc" : "amt_asc";
                 ViewBag.FacultyName = sortOrder == "fname_asc" ? "fname_desc" : "fname_asc";
                  ViewBag.IssuedDate = sortOrder == "date_asc" ? "date_desc" : "date_asc";

                var students = from s in studentContext
                               select s;


                if (!string.IsNullOrEmpty(searchString))
                {
                    students = students.Where(s => s.Student.Name.Contains(searchString)
                                           || s.Payment.Type.Contains(searchString)
                                           || s.Student.Address.Contains(searchString)
                                           || s.Payment.Amount.ToString().Contains(searchString)
                                           || s.Student.PhoneNo.ToString().Contains(searchString)
                                           || s.Faculty.Name.Contains(searchString)
                                           || s.IssuedDate.ToString().Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "studentname_desc":
                        students = students.OrderByDescending(s => s.Student.Name);
                        break;
                    case "fname_desc":
                        students = students.OrderByDescending(s => s.Faculty.Name);
                        break;
                    case "fname_asc":
                        students = students.OrderBy(s => s.Faculty.Name);
                        break;
                    case "amt_desc":
                        students = students.OrderByDescending(s => s.Payment.Amount);
                        break;
                    case "amt_asc":
                        students = students.OrderBy(s => s.Payment.Amount);
                        break;
                    case "addressname_desc":
                        students = students.OrderByDescending(s => s.Student.Address);
                        break;
                    case "addressname_asc":
                        students = students.OrderBy(s => s.Student.Address);
                        break;
                    case "type_desc":
                        students = students.OrderByDescending(s => s.Payment.Type);
                        break;
                    case "type_asc":
                        students = students.OrderBy(s => s.Payment.Type);
                        break;
                    case "phonenoname_desc":
                        students = students.OrderByDescending(s => s.Student.PhoneNo);
                        break;
                    case "phonenoname_asc":
                        students = students.OrderBy(s => s.Student.PhoneNo);
                        break;
                    case "date_desc":
                        students = students.OrderByDescending(s => s.IssuedDate);
                        break;
                    case "date_asc":
                        students = students.OrderBy(s => s.IssuedDate);
                        break;


                    default:
                        students = students.OrderBy(s => s.Student.Name);
                        break;
                }
                return View(students);
            
         //   return View(await studentContext.ToListAsync());
        }

        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration
                .Include(r => r.Faculty)
                .Include(r => r.Payment)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            var student = _context.Student.ToList();
            student.Insert(0, new Student { Name = "Select StudentName" });
            ViewBag.Student = student;

            var payment = _context.Payment.ToList();
            payment.Insert(0, new Payment { Type = "Select PaymentType" });
            ViewBag.Payment = payment;

            var faculty = _context.Faculty.ToList();
            faculty.Insert(0, new Faculty { Name = "Select FacultyName" });
            ViewBag.Faculty = faculty;


            var studentAddress = _context.Student.ToList();
            ViewBag.StudentAddress = studentAddress.Select(X => new SelectListItem
            {
                Text = X.Address.ToString(),
                Value = X.Id.ToString()
            });

            var studentPhoneNo = _context.Student.ToList();
            ViewBag.StudentPhoneNo = studentAddress.Select(X => new SelectListItem
            {
                Text = X.PhoneNo.ToString(),
                Value = X.Id.ToString()
            });

            var paymentAmount = _context.Payment.ToList();
            ViewBag.PaymentAmount = paymentAmount.Select(X => new SelectListItem
            {
                Text = X.Amount.ToString(),
                Value = X.Id.ToString()
            });

            ViewData["Id"] = new SelectList(_context.Faculty, "Id", "Id");
            ViewData["Id"] = new SelectList(_context.Payment, "Id", "Id");
            ViewData["Id"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( RegistrationViewModel registrationVM)
        {
            if (registrationVM.StudentId == "0")
            {
                var stu = _context.Student.ToList();
                stu.Insert(0, new Student { Name = "Select Name" });
                ViewBag.Student = stu;
                ModelState.AddModelError("Id", "Select Name");
                return View(registrationVM);
            }


            if (registrationVM.PaymentId == "0")
            {
                var payments = _context.Payment.ToList();
                payments.Insert(0, new Payment { Type = "Select Type" });
                ViewBag.Payment = payments;
                ModelState.AddModelError("Id", "Select type of payments");
                return View(registrationVM);
            }

            if (registrationVM.FacultyId == "0")
            {
                var fac = _context.Faculty.ToList();
                fac.Insert(0, new Faculty { Name = "Select Name" });
                ViewBag.Faculty = fac;
                ModelState.AddModelError("Id", "Select Faculty");
                return View(registrationVM);
            }

            if (ModelState.IsValid)
            {

             
                    var registration = new Registration
                    {
                        IssuedDate = registrationVM.IssuedDate,
                        StudentId = registrationVM.StudentId,
                        FacultyId = registrationVM.FacultyId,
                        PaymentId = registrationVM.PaymentId,

                    };

                    _context.Add(registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                

        
            return View(registrationVM);

            #region defualt code for httppost Create
            //if (ModelState.IsValid)
            //{
            //    _context.Add(registration);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["Id"] = new SelectList(_context.Faculty, "Id", "Id", registration.Id);
            //ViewData["Id"] = new SelectList(_context.Payment, "Id", "Id", registration.Id);
            //ViewData["Id"] = new SelectList(_context.Student, "Id", "Id", registration.Id);
            //return View(registration);
            #endregion
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var student = _context.Student.ToList();
                ViewBag.Student = student.Select(X => new SelectListItem
                {
                    Text = X.Name,
                    Value = X.Id.ToString()
                });
                var address = _context.Student.ToList();
                ViewBag.StudentAddress = address.Select(X => new SelectListItem
                {

                    Text = X.Address,
                    Value = X.Id.ToString()

                });
                var phone = _context.Student.ToList();
                ViewBag.StudentPhoneNo = phone.Select(X => new SelectListItem
                {

                    Text = X.PhoneNo.ToString(),
                    Value = X.Id.ToString()

                });


                var payment = _context.Payment.ToList();
                ViewBag.Payment = payment.Select(X => new SelectListItem
                {
                    Text = X.Type.ToString(),
                    Value = X.Id.ToString()
                });
                var paymentAmount = _context.Payment.ToList();
                ViewBag.PaymentAmount = paymentAmount.Select(X => new SelectListItem
                {
                    Text = X.Amount.ToString(),
                    Value = X.Id.ToString()
                });

                var fac = _context.Faculty.ToList();
                ViewBag.Faculty = fac.Select(X => new SelectListItem
                {

                    Text = X.Name,
                    Value = X.Id.ToString()

                });
           



                var registration = await _context.Registration.FindAsync(id);
                return View(registration);
                #region default code
                //if (id == null)
                //{
                //    return NotFound();
                //}

                //var registration = await _context.Registration.FindAsync(id);
                //if (registration == null)
                //{
                //    return NotFound();
                //}
                //ViewData["Id"] = new SelectList(_context.Faculty, "Id", "Id", registration.Id);
                //ViewData["Id"] = new SelectList(_context.Payment, "Id", "Id", registration.Id);
                //ViewData["Id"] = new SelectList(_context.Student, "Id", "Id", registration.Id);
                //return View(registration);
                #endregion
            }
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PaymentId,FacultyId,StudentId,IssuedDate,Id")] Registration registration)
        {
            if (id != registration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.Id))
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
            ViewData["Id"] = new SelectList(_context.Faculty, "Id", "Id", registration.Id);
            ViewData["Id"] = new SelectList(_context.Payment, "Id", "Id", registration.Id);
            ViewData["Id"] = new SelectList(_context.Student, "Id", "Id", registration.Id);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration
                .Include(r => r.Faculty)
                .Include(r => r.Payment)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var registration = await _context.Registration.FindAsync(id);
            _context.Registration.Remove(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(string id)
        {
            return _context.Registration.Any(e => e.Id == id);
        }
    }
}
