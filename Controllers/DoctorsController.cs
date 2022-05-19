using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Repository;

namespace HospitalProject.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ISpecialityRepository _specRepo;
        private readonly IDoctorRepository _docRepo;

        public DoctorsController(IDoctorRepository docRepo, ISpecialityRepository specRepo)
        {
            _specRepo = specRepo;
            _docRepo = docRepo;
        }

        // GET: Doctors
        public IActionResult Index()
        {
            return View(_docRepo.GetAll());
        }

        // GET: Doctors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _docRepo.Find(id.GetValueOrDefault());
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> doctorList = _specRepo.GetAll().Select(d => new SelectListItem{
                Text = d.specialityName,
                Value = d.specialityId.ToString()
            });
            ViewBag.DoctorsList = doctorList;
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("doctorId,firstName,lastName,specialityId")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _docRepo.Add(doctor);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["specialityId"] = new SelectList(_docRepo.GetSpecialities(), "specialityId", "specialityId", doctor.specialityId);
            //ViewData["specialityId"] = new SelectList(_docRepo.GetSpecialities(), "specialityName", "specialityId", doctor.specialityId);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _docRepo.Find(id.GetValueOrDefault());
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["specialityId"] = new SelectList(_docRepo.GetSpecialities(), "specialityId", "specialityId", doctor.specialityId);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("doctorId,firstName,lastName,specialityId")] Doctor doctor)
        {
            if (id != doctor.doctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _docRepo.Update(doctor);
                return RedirectToAction(nameof(Index));
            }
            ViewData["specialityId"] = new SelectList(_docRepo.GetSpecialities(), "specialityId", "specialityId", doctor.specialityId);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _docRepo.Remove(id.GetValueOrDefault());


            return RedirectToAction(nameof(Index));
        }

    }
}
