using HospitalProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HospitalProject.Repository
{
    public interface ISpecialityRepository
    {
        Speciality Find(int id);
        List<Speciality> GetAll();
        Speciality Add(Speciality speciality);
        Speciality Update(Speciality speciality);
        void Remove(int id);
        public List<Speciality> GetSpecialities();
    }
}
