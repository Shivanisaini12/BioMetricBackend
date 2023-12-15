using BioMatricAttendence.Models;

namespace BioMatricAttendence.Repository
{
    public interface IUser
    {
        void InsertUser(User entity);
        void InsertDevice(Device entity);
        bool UserAvail(string email);
        string UserLogin(Login user);
        IEnumerable<User> GetUsers(string EmailId);
        IEnumerable<Department> GetDepartment();
        IEnumerable<Designation> GetDesignation();
        void InsertEmpProfile(EmpProfile entity);
        EmpProfile EmpAvail(EmpProfile EmpEmail);
        void Update(EmpProfile item);
        IEnumerable<Device> GetDevice();
        string GetEmpById(string id);
        AttendenceReport GetcheckinTime(string id);
        bool GetDeviceInfo(string Device_Id);
        void InsertAttendenceReport(AttendenceReport entity);
        AttendenceReport CheckAttendenceEmp(string EmpId, string Tdate);
        void AttendenceUpdate(AttendenceReport item);
        IEnumerable<AttendenceReport> GetAttendeceReport();
        IEnumerable<Attendence> GetAttendance();
    }
}
