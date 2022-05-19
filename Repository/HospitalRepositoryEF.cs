using HospitalProject.Data;
using HospitalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace HospitalProject.Repository
{
    public class HospitalRepositoryEF : IHospitalRepository
    {
        private readonly ApplicationDbContext _db;

        public HospitalRepositoryEF(ApplicationDbContext db)
        {
            _db = db;
        }


        public Hospital Add(Hospital hospital)
        {
            _db.hospital.Add(hospital);
            _db.SaveChanges();
            return hospital;
        }

        public Hospital Find(int id)
        {
            return _db.hospital.FirstOrDefault(u => u.hospitalId == id);
        }

        public List<Hospital> GetAll()
        {
            return _db.hospital.ToList();
        }

        public void Remove(int id)
        {
            Hospital h = _db.hospital.FirstOrDefault(u => u.hospitalId == id);
            _db.Remove(h);
            _db.SaveChanges();
            return;
        }

        public Hospital Update(Hospital hospital)
        {
            _db.hospital.Update(hospital);
            _db.SaveChanges();
            return hospital;
        }
    }
}
