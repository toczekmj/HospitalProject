using HospitalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalProject.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Hospital> hospital { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Speciality> speciality { get; set; }
        public DbSet<Doctor> doctor { get; set; }
        public DbSet<doctor_department> doctor_department { get; set; }


    }
}
