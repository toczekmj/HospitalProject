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
using Microsoft.AspNetCore.Authorization;

namespace HospitalProject.Controllers
{

    public class DoctorsController : Controller
    {
        private readonly ISpecialityRepository _specRepo;
        private readonly IDoctorRepository _docRepo;
        private readonly IDepartmentRepository _depRepo;

        public DoctorsController(IDoctorRepository docRepo, ISpecialityRepository specRepo, IDepartmentRepository depRepo)
        {
            _specRepo = specRepo;
            _docRepo = docRepo;
            _depRepo = depRepo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctors = _docRepo.GetDoctorsWithSpecialities();
            return View(doctors);
        }

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

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> doctorList = _specRepo.GetAll().Select(d => new SelectListItem{
                Text = d.specialityName,
                Value = d.specialityId.ToString()
            });
            
            //IEnumerable<SelectListItem> depList = _depRepo.GetAll().Select(d => new SelectListItem{
            //    Text = d.departmentName,
            //    Value = d.departmentId.ToString()
            //});
            //ViewBag.DepartmentList = depList;
            ViewBag.DoctorsList = doctorList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("doctorId,firstName,lastName,specialityId")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _docRepo.Add(doctor);
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

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
            //ViewData["specialityId"] = new SelectList(_docRepo.GetSpecialities(), "specialityId", "specialityId", doctor.specialityId);

            IEnumerable<SelectListItem> doctorList = _specRepo.GetAll().Select(d => new SelectListItem
            {
                Text = d.specialityName,
                Value = d.specialityId.ToString()
            });
            ViewBag.DoctorsList = doctorList;

            return View(doctor);
        }

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
