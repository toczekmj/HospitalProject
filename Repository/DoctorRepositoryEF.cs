using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalProject.Repository
{
    public class DoctorRepositoryEF : IDoctorRepository
    {
        private readonly ApplicationDbContext _db;

        public DoctorRepositoryEF(ApplicationDbContext db)
        { 
            _db = db;
        }


        public Doctor Add(Doctor doctor)
        {
            _db.doctor.Add(doctor);
            _db.SaveChanges();
            return doctor;
        }

        public Doctor Find(int id)
        {
            return _db.doctor.FirstOrDefault(u => u.doctorId == id);
        }

        public List<DoctorVM> GetAll()
        {
            return null;
            //return _db.doctor.ToList();
        }

        public void Remove(int id)
        {
            Doctor d = _db.doctor.FirstOrDefault(u => u.doctorId == id);
            _db.Remove(d);
            _db.SaveChanges();
            return;
        }

        public Doctor Update(Doctor doctor)
        {
            _db.doctor.Update(doctor);
            _db.SaveChanges();
            return doctor;
        }

        public List<Speciality> GetSpecialities()
        {
            return _db.speciality.ToList();
        }
    }
}
