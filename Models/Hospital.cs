using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Hospital
    {
        [Key]
        [Display(Name = "ID")]
        public int hospitalId { get; set; }
        [Display(Name = "Hospital Name")]
        public string hospitalName { get; set; }
        public List<Department> departmentList { get; set; }
    }
}
