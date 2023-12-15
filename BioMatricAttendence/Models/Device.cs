using System.ComponentModel.DataAnnotations;

namespace BioMatricAttendence.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Device_Id { get; set; }
        public string DeviceName { get; set; }
        public string? dateTimeHour { get; set; }
    }
}
