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
using Microsoft.AspNetCore.Authorization;

namespace HospitalProject.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class DepartmentsController : Controller
    {
        private readonly Repository.IDepartmentRepository _depCont;
        private readonly IHospitalRepository _hosRep;
        private readonly IDoctorRepository _docRep;

        public DepartmentsController(Repository.IDepartmentRepository depCont, Repository.IHospitalRepository hosRep, Repository.IDoctorRepository docRep)
        {
            _depCont = depCont;
            _hosRep = hosRep;
            _docRep = docRep;
        }

        public IActionResult Index()
        {
            List<Department> departments = _depCont.GetDepartmentsWithHospitals();
            return View(departments);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _depCont.Find(id.GetValueOrDefault());
            Department helper = _depCont.GetDepartmentsWithHospitals().Where(p => p.departmentId == id.GetValueOrDefault()).ToList().FirstOrDefault();
            department.hospital = helper.hospital;

            var doctors = _depCont.GetDoctors(id.GetValueOrDefault());
            department.doctorsList = doctors;

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        public IActionResult Create()
        {
            //ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId");
            IEnumerable<SelectListItem> hospitalList = _hosRep.GetAll().Select(d => new SelectListItem
            {
                Text = d.hospitalName,
                Value = d.hospitalId.ToString()
            });
            ViewBag.HospitalList = hospitalList;
            return View();
        }

        public IActionResult AddDoctor(int? id)
        {
            IEnumerable<SelectListItem> doctorList = _docRep.GetAll().Select(d => new SelectListItem
            {
                Text = d.firstName + " " + d.lastName + " " + d.specialityName,
                Value = d.doctorId.ToString()
            });
            ViewBag.DoctorList = doctorList;

            var a = _depCont.Find(id.GetValueOrDefault());

            return View(a);
        }

        [HttpPost]
        public IActionResult AddDoctor(int? departmentId, int? doctorId)
        {
            if(departmentId != null && doctorId != null)
                _depCont.AddDoctor(doctorId.GetValueOrDefault(), departmentId.GetValueOrDefault());
            return RedirectToAction("Details", "Departments", new {id = departmentId.GetValueOrDefault()});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("departmentId,departmentName,hospitalId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _depCont.Add(department);
                return RedirectToAction(nameof(Index));
            }
            ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId", department.hospitalId);
            return View(department);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _depCont.Find(id.GetValueOrDefault());
            if (department == null)
            {
                return NotFound();
            }
            //ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId", department.hospitalId);
            IEnumerable<SelectListItem> hospitalList = _hosRep.GetAll().Select(d => new SelectListItem
            {
                Text = d.hospitalName,
                Value = d.hospitalId.ToString()
            });
            ViewBag.HospitalList = hospitalList;
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("departmentId,departmentName,hospitalId")] Department department)
        {
            if (id != department.departmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _depCont.Update(department);
                return RedirectToAction(nameof(Index));
            }
            ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId", department.hospitalId);
            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _depCont.Remove(id.GetValueOrDefault());

            return RedirectToAction(nameof(Index));
        }
    }
}
