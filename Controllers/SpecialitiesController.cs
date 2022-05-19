using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Repository;
using Microsoft.Extensions.Configuration;

namespace HospitalProject.Controllers
{
    public class SpecialitiesController : Controller
    {
        private readonly ISpecialityRepository _specRepo;

        public SpecialitiesController(ISpecialityRepository spec)
        {
            _specRepo = spec;
        }

        // GET: Specialities
        public IActionResult Index()
        {
            return View(_specRepo.GetAll());
        }

        // GET: Specialities/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = _specRepo.Find(id.GetValueOrDefault());
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // GET: Specialities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("specialityId,specialityName")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                _specRepo.Add(speciality);
                return RedirectToAction(nameof(Index));
            }
            return View(speciality);
        }

        // GET: Specialities/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = _specRepo.Find(id.GetValueOrDefault());
            if (speciality == null)
            {
                return NotFound();
            }
            return View(speciality);
        }

        // POST: Specialities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("specialityId,specialityName")] Speciality speciality)
        {
            if (id != speciality.specialityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _specRepo.Update(speciality);
                return RedirectToAction(nameof(Index));
            }
            return View(speciality);
        }

        // GET: Specialities/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _specRepo.Remove(id.GetValueOrDefault());

            return RedirectToAction(nameof(Index));
        }
    }
}
