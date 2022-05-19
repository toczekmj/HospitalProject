using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Department
    {   
        
        //[Display(Name = "ID")]
        public int departmentId { get; set; }
        [Display(Name = "Department Name")]
        public string departmentName { get; set; }
        [Display(Name = " Hospital ID")]
        public int hospitalId { get; set; }
        [Display(Name = "Hospital")]
        public Hospital hospital { get; set; }
    }
}
