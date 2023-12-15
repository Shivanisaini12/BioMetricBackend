using System.ComponentModel.DataAnnotations;

namespace BioMatricAttendence.Models
{
    public class Department
    {
        [Key]
        public int Dept_Id { get; set; }
        public string DepartmentName { get; set; }
    }
}
