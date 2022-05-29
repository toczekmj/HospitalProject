using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models
{
    [Table("Department")]
    public class Department
    {   
        
        [Display(Name = "ID")]
        [ExplicitKey]
        public int departmentId { get; set; }
        [Display(Name = "Department Name")]
        public string departmentName { get; set; }
        [Display(Name = " Hospital ID")]
        public int hospitalId { get; set; }
        [Display(Name = "Hospital")]
        public virtual Hospital hospital { get; set; }
        public List<Doctor> doctorsList { get; set; }
        public int doctorId { get; set; }

    }
}
