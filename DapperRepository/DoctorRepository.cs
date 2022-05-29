using Dapper;
using HospitalProject.Models;
using HospitalProject.Repository;
using HospitalProject.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HospitalProject.DapperRepository
{
    public class DoctorRepository : IDoctorRepository
    {
        private IDbConnection db;

        public DoctorRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


        public Doctor Add(Doctor doctor)
        {
            var sql = "addproc";
            var id = db.Query<int>(sql, new
            {
                @firstName = doctor.firstName,
                @lastName = doctor.lastName,
                @specialityId = doctor.specialityId
            }, commandType: CommandType.StoredProcedure).Single();
            doctor.doctorId = id;


            //    var sql2 = "INSERT INTO doctor_department (doctorId, departmentId) VALUES(@doctorId, @departmentId);";
            //db.Execute(sql2);


            return doctor;
        }

        public Doctor Find(int id)
        {
            var procname = "findproc";
            var result = db.Query<Doctor>(procname, new
            {
                @id = id
            }, commandType: CommandType.StoredProcedure).Single();
            return result;
        }

        public List<Doctor> GetAll()
        {
            var procname = "getallproc";
            var doctors = db.Query<Doctor>(procname, commandType: CommandType.StoredProcedure).ToList();
            return doctors;
        }

        public void Remove(int id)
        {
            var procname = "deleteproc";
            db.Execute(procname, new
            {
                @id = id
            }, commandType: CommandType.StoredProcedure);
        }

        public Doctor Update(Doctor doctor)
        {
            var sql = "updateproc";
            db.Execute(sql, new
            {
                @doctorId = doctor.doctorId,
                @firstName = doctor.firstName,
                @lastName = doctor.lastName,
                @specialityId = doctor.specialityId
            }, commandType: CommandType.StoredProcedure);
            return doctor;
        }

        public List<Speciality> GetSpecialities()
        {
            var sql = "SELECT specialityId FROM speciality";
            var temp =  db.Query<Speciality>(sql).ToList();
            return temp;
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
