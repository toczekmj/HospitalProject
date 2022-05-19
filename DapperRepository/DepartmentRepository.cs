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
            //var temp = db.GetAll<Department>().ToList();

            //return department;


        }

        public Department Find(int id)
        {
            var sql = "SELECT * FROM department WHERE departmentId = @id";
            return db.Query<Department>(sql, new { @id = id }).Single();
            //return db.Get<Department>(id);
        }

        public List<DepartmentVM> GetAll()
        {
            var sql = "SELECT d.departmentId, d.departmentName, h.hospitalName FROM department d inner join hospital h on h.hospitalId = d.hospitalId";
            return db.Query<DepartmentVM>(sql).ToList();
            //return db.GetAll<DepartmentVM>().ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM department WHERE departmentId = @Id";
            db.Execute(sql, new
            {
                @id = id
            });
            //db.Delete<Department>(new Department { departmentId = id });
        }

        public Department Update(Department department)
        {
            var sql = "UPDATE department SET departmentName = @departmentName , hospitalId = @hospitalId WHERE departmentId = @departmentId";
            db.Execute(sql, new
            {
                @departmentName = department.departmentName,
                @hospitalId = department.hospitalId,
                @departmentId = department.departmentId
            });
            return department;
            //db.Update<Department>(department);
            //return department;
        }

        public List<Hospital> GetHospitals()
        {
            var sql = "SELECT hospitalId FROM hospital";
            var temp = db.Query<Hospital>(sql).ToList();
            return temp;
        }
    }
}
