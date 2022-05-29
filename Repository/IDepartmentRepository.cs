using HospitalProject.Models;
using HospitalProject.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HospitalProject.Repository
{
    public interface IDepartmentRepository
    {
        Department Find(int id);
        List<DepartmentVM> GetAll();
        Department Add(Department department);
        Department Update(Department department);
        void Remove(int id);
        public List<Hospital> GetHospitals();
        public List<Department> GetDepartmentsWithHospitals();
        public List<Doctor> GetDoctors(int id);
        public void AddDoctor(int docid, int depid);
    }
}
