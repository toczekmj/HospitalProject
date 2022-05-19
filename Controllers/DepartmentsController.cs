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

        // GET: Departments
        public IActionResult Index()
        {
            //var applicationDbContext = _context.department.Include(d => d.hospital);
            return View(_depCont.GetAll());
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Departments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var department = await _context.department
            //    .Include(d => d.hospital)
            //    .FirstOrDefaultAsync(m => m.departmentId == id);
            var department = _depCont.Find(id.GetValueOrDefault());
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("departmentId,departmentName,hospitalId")] Department department)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(department);
                //await _context.SaveChangesAsync();
                _depCont.Add(department);
                return RedirectToAction(nameof(Index));
            }
            ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId", department.hospitalId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var department = await _context.department.FindAsync(id);
            var department = _depCont.Find(id.GetValueOrDefault());
            if (department == null)
            {
                return NotFound();
            }
            ViewData["hospitalId"] = new SelectList(_depCont.GetHospitals(), "hospitalId", "hospitalId", department.hospitalId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Departments/Delete/5
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
