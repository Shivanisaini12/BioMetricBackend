using Microsoft.EntityFrameworkCore;

namespace BioMatricAttendence.Models.DB
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Usertbl { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Device> Device_tbl { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<EmpProfile> EmpProfile { get; set; }
        public DbSet<AttendenceReport> AttendenceReport { get; set; }
        public DbSet<Attendence> Attendence { get; set; }
        
    }
}
