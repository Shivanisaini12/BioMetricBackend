using System.ComponentModel.DataAnnotations;

namespace BioMatricAttendence.Models
{
    public class EmpProfile
    {
        [Key]
        public int Id { get; set; }
        public string Emp_Email { get; set; }
        public int DesignationId { get; set; }
        public string EmpId { get; set; }
        public int DepartmentId { get; set; }
    }
}
