using BioMatricAttendence.Models;
using BioMatricAttendence.Models.DB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Cryptography.X509Certificates;

namespace BioMatricAttendence.Repository
{
    public class UserServices : IUser
    { 
        private readonly ApplicationContext _context;
        public UserServices(ApplicationContext context)
        {
            this._context = context;
        }
        public void InsertUser(User entity)
        {
            _context.Usertbl.Add(entity);
            _context.SaveChanges();
        }
        public bool UserAvail(string email) 
        {
            return _context.Usertbl.Any(a=> a.EmailId == email);
        }
        public string UserLogin(Login user)
        {
            return _context.Usertbl.Where(s => s.EmailId == user.EmailId && s.Password == user.Password).FirstOrDefault().ToString();
        }
        public IEnumerable<User> GetUsers(string EmailId)
        {
            var clientIdParameter = new SqlParameter("@EmailId", EmailId);

            return _context.Usertbl.FromSqlRaw("EXEC GetAllEmpbyId @EmailId", clientIdParameter)
                .ToList();

            //return _context.Usertbl.FromSqlRaw("EXEC GetAllEmpbyId", Emp_id).ToList();
        }
        public void InsertDevice(Device entity)
        {
            _context.Device_tbl.Add(entity);
            _context.SaveChanges();
        }
        public IEnumerable<Department> GetDepartment()
        {
            return _context.Department.ToList();
        }
        public IEnumerable<Designation> GetDesignation()
        {
            return _context.Designation.ToList();
        }
        public void InsertEmpProfile(EmpProfile entity)
        {
            _context.EmpProfile.Add(entity);
            _context.SaveChanges();
        }
        public EmpProfile EmpAvail(EmpProfile EmpEmail)
        {
          return _context.EmpProfile.Where(x => x.Emp_Email == EmpEmail.Emp_Email).FirstOrDefault();
        }
        public void Update(EmpProfile item)
        {
            var entity = _context.EmpProfile.Where(s => s.Emp_Email == item.Emp_Email).FirstOrDefault();
            if (entity == null)
            {
                return;
            }
            _context.Entry(entity).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }
        public IEnumerable<Device> GetDevice()
        {
            return _context.Device_tbl.ToList();
        }
        public string GetEmpById(string id) 
        {
            var data = from  a in _context.EmpProfile
                          join b in _context.AttendenceReport on a.EmpId equals b.EmpId
                           where(b.EmpId == id) select new {b.InTime  , b.date};
            if (data!=null && data.Count() > 0 && data.FirstOrDefault().InTime!= null && data.FirstOrDefault().date == DateTime.Now.ToString("dd/MM/yyyy"))
            {
               // string checin = data.FirstOrDefault().InTime.ToString();
                return data.FirstOrDefault().InTime.ToString();
            }
            //return false;

            return this._context.EmpProfile.Any(x => x.EmpId == id).ToString();
        }
        public AttendenceReport GetcheckinTime(string id)
        {
            return this._context.AttendenceReport.Where(x => x.EmpId == id).FirstOrDefault();
        }
        public bool GetDeviceInfo(string Device_Id)
        {
            return _context.Device_tbl.Any(s => s.Device_Id == Device_Id);
        }
        public void InsertAttendenceReport(AttendenceReport entity)
        {
            _context.AttendenceReport.Add(entity);
            _context.SaveChanges();
        }
        public  AttendenceReport  CheckAttendenceEmp(string EmpId, string Tdate)
        {
           // var user = (from u in _context.AttendenceReport where u.EmpId == EmpId && u.date == Tdate select u).FirstOrDefault();
            return _context.AttendenceReport.Where(x => x.EmpId == EmpId && x.date == Tdate).FirstOrDefault();
        }
        public void AttendenceUpdate(AttendenceReport item)
        {
            var entity = _context.AttendenceReport.Where(s => s.Id == item.Id).FirstOrDefault();
            if (entity == null)
            {
                return;
            }
            _context.Entry(entity).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }
        public IEnumerable<AttendenceReport> GetAttendeceReport()
        {
            return _context.AttendenceReport.ToList();
        }
        public IEnumerable<Attendence> GetAttendance()
        {
            try 
            { 
                return _context.Attendence.FromSqlRaw("EXEC SP_AttendanceReport").ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        //public async Task<IEnumerable<AttendenceReport>> GetPeople()
        //{
        //    IEnumerable<AttendenceReport> people = await (from p in _context.AttendenceReport
        //                                                  select new AttendenceReport
        //                                                {
        //                                                    Id = p.Id,
        //                                                    FirstName = p.FirstName,
        //                                                    LastName = p.LastName,
        //                                                    Age = p.Age,
        //                                                    Address = p.Address,
        //                                                    City = p.City,
        //                                                    StateAbbrev = p.StateAbbrev,
        //                                                    ZipCode = p.ZipCode,
        //                                                    Photo = p.Photo,
        //                                                    Interests = new List<string>()
        //                                                }).ToListAsync();

        //    foreach (AttendenceReport person in people)
        //    {
        //        person.Interests = (from iint in _db.Interests
        //                            join n in _db.PersonInterests on iint.Id equals n.InterestId
        //                            where n.PersonId == person.Id
        //                            select iint.InterestName).ToList();
        //    }
        //    return people;
        //}
    }
}
