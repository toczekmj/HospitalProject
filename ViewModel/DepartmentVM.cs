using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.ViewModel
{
    public class DepartmentVM
    {
        [ExplicitKey]
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public string hospitalName { get; set; }
    }
}
