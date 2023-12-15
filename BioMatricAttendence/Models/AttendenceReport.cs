using System.ComponentModel.DataAnnotations;

namespace BioMatricAttendence.Models
{
    public class AttendenceReport
    {
        [Key]
        public int? Id { get; set; }
        public string EmpId { get; set; }
        public string Device_Id { get; set; }
        public string InTime { get; set; }
        public string ? OutTime { get; set; }
        public string date { get; set; }
    }
}
