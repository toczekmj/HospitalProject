using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalProject.Data;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly Repository.IDepartmentRepository _depCont;

        public DepartmentsController(Repository.IDepartmentRepository depCont)
        {
            _depCont = depCont;
        }

        public IActionResult Index()
        {
            List<Department> departments = _depCont.GetDepartmentsWithHospitals();
            return View(departments);
            //return View(_depCont.GetAll());
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
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        public IActionResult Create()
        {
            ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId");
            return View();
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
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId", department.hospitalId);
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
