using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models
{
    public class Doctor
    {
        [Display(Name = "ID")]
        public int doctorId { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Speciality ID")]
        public int specialityId { get; set; }
        [Display(Name = "Speciality")]
        public virtual Speciality speciality { get; set; }
    }
}
