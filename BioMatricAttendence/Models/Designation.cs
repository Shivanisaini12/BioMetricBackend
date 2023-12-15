using System.ComponentModel.DataAnnotations;

namespace BioMatricAttendence.Models
{
    public class Designation
    {
        [Key]
        public int Desig_Id { get; set; }
        public string DesignationName { get; set; }
    }
}
