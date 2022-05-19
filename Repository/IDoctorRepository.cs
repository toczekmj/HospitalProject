using HospitalProject.Models;
using HospitalProject.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HospitalProject.Repository
{
    public interface IDoctorRepository
    {
        Doctor Find(int id);
        List<DoctorVM> GetAll();
        Doctor Add(Doctor doctor);
        Doctor Update(Doctor doctor);
        void Remove(int id);
        public List<Speciality> GetSpecialities();
    }
}
