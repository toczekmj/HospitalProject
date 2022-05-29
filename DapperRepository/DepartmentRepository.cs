using Dapper;
using Dapper.Contrib.Extensions;
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
    public class DepartmentRepository : IDepartmentRepository
    {
        private IDbConnection db;

        public DepartmentRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


        public Department Add(Department department)
        {


            var sql = "INSERT INTO department (departmentName, hospitalId) VALUES(@departmentName,@hospitalId);"
    + "SELECT CAST(SCOPE_IDENTITY() as int);";


            var id = db.Query<int>(sql, new
            {
                @departmentName = department.departmentName,
                @hospitalId = department.hospitalId
            }).Single();

            department.departmentId = id;

            return department;
        }

        public Department Find(int id)
        {
            return db.Get<Department>(id);
        }

        public List<DepartmentVM> GetAll()
        {
            //var sql = "SELECT d.departmentId, d.departmentName, h.hospitalName FROM department d inner join hospital h on h.hospitalId = d.hospitalId";
            //return db.Query<DepartmentVM>(sql).ToList();
            var dep = db.GetAll<Department>().ToList();
            var hosp = db.GetAll<Hospital>().ToList();
            var f = from d in dep
                    join h in hosp
                    on d.hospitalId equals h.hospitalId
                    select new DepartmentVM()
                    {
                        departmentId = d.departmentId,
                        departmentName = d.departmentName,
                        hospitalName = h.hospitalName
                    };

            //hosp.GroupJoin(dep, (hosp => hosp.hospitalId), (dep => dep.hospitalId), (hospital, departments) => new {  });

            return f.ToList();
        }

        public void Remove(int id)
        { 
            db.Delete<Department>(new Department {  departmentId = id});
        }

        public Department Update(Department department)
        {
            var sql = "UPDATE department SET departmentName = @departmentName, hospitalId = @hospitalId WHERE departmentId = @departmentId;";
            db.Execute(sql, new
            {
                @departmentName = department.departmentName,
                @hospitalId = department.hospitalId,
                @departmentId = department.departmentId,
            });

            return department;
        }

        public List<Doctor> GetDoctors(int id)
        {
            var sql = "SELECT d.doctorId, d.firstName, d.lastName, spec.specialityName "
                + "FROM doctor d "
                + "INNER JOIN doctor_department dd ON d.doctorId = dd.doctorId "
                + "INNER JOIN department dep ON dep.departmentId = dd.departmentId "
                + "INNER JOIN speciality spec ON spec.specialityId = d.specialityId "
                + "WHERE dd.departmentId = @id;";
            var res = db.Query<Doctor>(sql, new {@id = id});
            return res.ToList();
            //return null;
        }

        public void AddDoctor(int docid, int depid)
        {
            var sql = "INSERT INTO doctor_department (doctorId, departmentId) VALUES (@docid, @depid)";
            db.Execute(sql, new
            {
                @docid = docid,
                @depid = depid
            });
        }

        public List<Hospital> GetHospitals()
        {
            return db.GetAll<Hospital>().ToList();
        }

        public List<Department> GetDepartmentsWithHospitals()
        {
            var sql = "SELECT d.*, h.* FROM department d INNER JOIN hospital h ON d.hospitalId = h.hospitalId;";
            var result = db.Query<Department, Hospital, Department>(sql, (dep, hosp) =>
            {
                dep.hospital = hosp;
                return dep;
            }, splitOn: "hospitalId").ToList();
            return result;
        }
    }
}
