using Dapper;
using HospitalProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HospitalProject.Repository
{
    public class BonusRepository : IBonusRepository
    {
        private IDbConnection db;
        public BonusRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public List<Doctor> GetDoctorsWithSpecialities()
        {
            var sql = "SELECT d.*, s.* FROM doctor d INNER JOIN speciality s ON d.specialityId = s.specialityId";
            var result = db.Query<Doctor, Speciality, Doctor>(sql,
                (doc, spec) => {
                    doc.speciality = spec;
                    return doc;
                }, splitOn: "specialityId");
            return result.ToList();
        }
    }
}
