using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models
{
    public class Speciality
    {
        [Display(Name = "ID")]
        public int specialityId { get; set; }
        [Display(Name = "Speciality")]
        public string specialityName { get; set; }
        public List<Doctor> doctorList { get; set; }
    }
}
