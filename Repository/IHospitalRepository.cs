using HospitalProject.Models;
using System.Collections.Generic;

namespace HospitalProject.Repository
{
    public interface IHospitalRepository
    {
        Hospital Find(int id);
        List<Hospital> GetAll();
        Hospital Add(Hospital hospital);
        Hospital Update(Hospital hospital);
        void Remove(int id);
    }
}
