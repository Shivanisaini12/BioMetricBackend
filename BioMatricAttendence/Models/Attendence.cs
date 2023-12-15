using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BioMatricAttendence.Models
{
    public class Attendence
    {
        [Key]
        //public int? Id { get; set; }
        public long rno { get; set; }
        public string EmpId { get; set; }
        public DateTime check_in { get; set; }
        public DateTime check_out { get; set;}

        //public int? Id { get; set; }
        //public string EmpId { get; set; }
        //public string? Device_Id { get; set; }
        //public string status { get; set; }
        //public DateTime created { get; set; }


    }
}
