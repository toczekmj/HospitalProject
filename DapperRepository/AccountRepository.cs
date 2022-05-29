using Dapper;
using Dapper.Contrib.Extensions;
using HospitalProject.Models;
using HospitalProject.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;

namespace HospitalProject.DapperRepository
{
    public class AccountRepository : IAccountRepository
    {
        private IDbConnection db;
        public AccountRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


        //login
        //create

        public User Login(string email, string password)
        {
            var sql = "SELECT * FROM Users WHERE Email = @email AND Password = @password";
            var user = db.Query<User>(sql, new { @email = email, @password = password}).SingleOrDefault();
            return user;
        }
        public User CheckLogin(string email, string password)
        {
            var sql = "SELECT * FROM Users WHERE Email = @email AND Password = @password";
            var t = db.Query<User>(sql, new { @email = email, @password = password});
            if (t.Count() == 0)
                return null;
            return t.Single();
        }

        public User Add(User user)
        {
            var sql = "INSERT INTO Users (Id, Email, Role, Password) VALUES(@id, @email, @role, @password);"
                + "SELECT CAST(SCOPE_IDENTITY() as int);";

            var s = "SELECT * FROM Users";
            var t = db.Query<User>(s).ToList();
            user.Id = t.Count();

            var id = db.Query<User>(sql, new
            {
                @id = user.Id + 1,
                @email = user.Email,
                @role = "default",
                @password = user.Password,
            }).Single();


            return user;
        }

        public bool Check(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @email";
            var t = db.Query<User>(sql, new { @email = email });
            return t.Count() == 0;
        }


        public User FindByToken(string token)
        {
            var sql = "SELECT * FROM Users WHERE Token = @Token";
            var t = db.Query<User>(sql, new { @Token = token });
            if (t.Count() == 0)
                return null;
            return t.Single();
        }

    }
}
