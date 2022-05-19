using Dapper;
using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HospitalProject.DapperRepository
{
    public class SpecialityRepository : ISpecialityRepository
    {
        private IDbConnection db;

        public SpecialityRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


        public Speciality Add(Speciality speciality)
        {
            var sql = "INSERT INTO speciality (specialityName) VALUES(@specialityName);"
                + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, new
            {
                @specialityName = speciality.specialityName
            }).Single();
            speciality.specialityId = id;
            return speciality;
        }

        public Speciality Find(int id)
        {
            var sql = "SELECT * FROM speciality WHERE specialityId = @id";
            return db.Query<Speciality>(sql, new { @id = id }).Single();
        }

        public List<Speciality> GetAll()
        {
            var sql = "SELECT * FROM speciality";
            var l = db.Query<Speciality>(sql).ToList();
            return l;
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM speciality WHERE specialityId = @Id";
            db.Execute(sql, new
            {
                @id = id
            });
        }

        public Speciality Update(Speciality speciality)
        {
            var sql = "UPDATE speciality SET specialityName = @specialityName WHERE specialityId = @specialityId";
            db.Execute(sql, new
            {
                @specialityName = speciality.specialityName,
                @specialityId = speciality.specialityId
            });
            return speciality;
        }

        public List<Speciality> GetSpecialities()
        {
            var sql = "SELECT specialityId FROM speciality";
            return db.Query<Speciality>(sql).ToList();
        }
    }
}
