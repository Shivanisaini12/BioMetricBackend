using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace BioMatricAttendence.Models
{
   
    public class User
    {
        [Key]
        public int Emp_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public String Password { get; set; }
        public string Contact { get; set; }

    }
}
