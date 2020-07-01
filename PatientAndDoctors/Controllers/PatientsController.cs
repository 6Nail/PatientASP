using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatientAndDoctors.Data;
using PatientAndDoctors.Models;

namespace PatientAndDoctors.Controllers
{
    public class PatientsController : Controller
    {
        private readonly Context _context;

        public PatientsController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Patients.ToList());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();

            return RedirectToAction("Index", "VisitHistories", new { Id = id });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,IIN,FIO,Address,Phone")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Id = Guid.NewGuid();
                _context.Add(patient);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,IIN,FIO,Address,Phone")] Patient patient)
        {
            if (id != patient.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(Guid? id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            else _context.Patients.Remove(patient);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(Guid id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
