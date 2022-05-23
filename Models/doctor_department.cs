using Microsoft.EntityFrameworkCore;

namespace HospitalProject.Models
{
    [Keyless]
    public class doctor_department
    {
        public int doctorId { get; set; }
        public int departmentId { get; set; }
        public Doctor doctor { get; set; }
        public virtual Department department { get; set; }
    }
}
