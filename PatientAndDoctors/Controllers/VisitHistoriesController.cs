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
    public class VisitHistoriesController : Controller
    {
        private readonly Context _context;

        public VisitHistoriesController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(Guid id)
        {
            if (id == null) return NotFound();
            var visitHistories = _context.VisitHistories.Include(v => v.Patient).Where(x => x.PatientId == id).ToList();
            return View(new VisitHistoryViewModel { PatientId = id, VisitHistories = visitHistories });
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();

            var visitHistory = _context.VisitHistories.Include(v => v.Patient).FirstOrDefault(m => m.Id == id);
            if (visitHistory == null) return NotFound();

            return View(visitHistory);
        }

        public IActionResult Create(Guid id)
        {
            return View(new VisitHistory { PatientId = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid id, [Bind("Doctor,DoctorName,Diagnosis,Complaint,VisitDate")] VisitHistory visitHistory)
        {
            if (ModelState.IsValid)
            {
                visitHistory.Id = Guid.NewGuid();
                visitHistory.PatientId = id;
                _context.Add(visitHistory);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { Id = visitHistory.PatientId });
            }
            return View(visitHistory);
        }

        // GET: VisitHistories/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var visitHistory = _context.VisitHistories.Find(id);
            if (visitHistory == null) return NotFound();
            return View(visitHistory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,PatientId,Doctor,DoctorName,Diagnosis,Complaint,VisitDate")] VisitHistory visitHistory)
        {
            if (id != visitHistory.Id) return NotFound();

            if (ModelState.IsValid)
            {

                _context.Update(visitHistory);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), new { Id = visitHistory.PatientId });
            }
            return View(visitHistory);
        }
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var visitHistory = _context.VisitHistories.FirstOrDefault(m => m.Id == id);
            if (visitHistory == null) return NotFound();

            _context.Remove(visitHistory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { Id = visitHistory.PatientId});
        }
    }
}
