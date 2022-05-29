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

namespace HospitalProject.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class HospitalsController : Controller
    {
        private readonly IHospitalRepository _hospRepo;

        public HospitalsController(IHospitalRepository hospRepo)
        {
            _hospRepo = hospRepo;
        }

        public IActionResult Index()
        {
            return View(_hospRepo.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = _hospRepo.GetDepartment(id.GetValueOrDefault());
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("hospitalId,hospitalName")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                _hospRepo.Add(hospital);
                return RedirectToAction(nameof(Index));
            }
            return View(hospital);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = _hospRepo.Find(id.GetValueOrDefault());
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("hospitalId,hospitalName")] Hospital hospital)
        {
            if (id != hospital.hospitalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _hospRepo.Update(hospital);
                return RedirectToAction(nameof(Index));
            }
            return View(hospital);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _hospRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }

    }
}
