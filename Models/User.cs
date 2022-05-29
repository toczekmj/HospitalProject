using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
