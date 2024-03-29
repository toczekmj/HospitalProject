﻿using Dapper;
using Dapper.Contrib.Extensions;
using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HospitalProject.DapperRepository
{
    public class HospitalRepository : IHospitalRepository
    {
        private IDbConnection db;

        public HospitalRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


        public Hospital Add(Hospital hospital)
        {
            var sql = "INSERT INTO hospital (hospitalName) VALUES(@hospitalName);"
                + "SELECT CAST(SCOPE_IDENTITY() as int);";


            var id = db.Query<int>(sql, new
            {
                @hospitalName = hospital.hospitalName
            }).Single();

            hospital.hospitalId = id;

            return hospital;
        }

        public Hospital Find(int id)
        {
            var sql = "SELECT * FROM hospital WHERE hospitalId = @id";
            return db.Query<Hospital>(sql, new { @id = id }).Single();
        }

        public List<Hospital> GetAll()
        {
            return db.GetAll<Hospital>().ToList();
        }

        public Hospital GetDepartment(int id)
        {
            var p = new
            {
                hospitalId = id
            };

            var sql = "SELECT * FROM hospital WHERE hospitalId = @hospitalId;"
                + "SELECT * FROM department WHERE hospitalId = @hospitalId;";

            Hospital hospital;
            using (var lists = db.QueryMultiple(sql, p))
            {
                hospital = lists.Read<Hospital>().ToList().FirstOrDefault();
                hospital.departmentList = lists.Read<Department>().ToList();
            }

            return hospital;
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM hospital WHERE hospitalId = @id";
            db.Execute(sql, new
            {
                @id = id
            });
        }
          
        public Hospital Update(Hospital hospital)
        {
            var sql = "UPDATE hospital SET hospitalName = @hospitalName WHERE hospitalId = @hospitalId;";
            db.Execute(sql, new
            {
                @hospitalName = hospital.hospitalName,
                @hospitalId = hospital.hospitalId,
            });
            return hospital;
        }
    }
}
