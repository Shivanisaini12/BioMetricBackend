using BioMatricAttendence.Models;
using BioMatricAttendence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BioMatricAttendence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EmployeeController : ControllerBase
    {
        public readonly IUser _repository;
        public EmployeeController(IUser repository)
        {
            _repository = repository;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create(User user)
        {
            if (_repository.UserAvail(user.EmailId) == true)
            {
                return Ok("AlreadyExisted");
            }
            _repository.InsertUser(user);
            return Ok("Success");
        }

        [HttpPost("LoginUser")]
        public IActionResult Login(Login login)
        {
            string userAvailable = _repository.UserLogin(login);
            if (userAvailable != null)
            {
                return Ok(login.EmailId);
            }
            return Ok("failure");
        }

         [HttpGet("GetUser")]
         //[HttpGet("GetUser/{EmailId}")]
        public IActionResult GetAllRecords(string EmailId)
        {
            var response = this._repository.GetUsers(EmailId);
            return Ok(response);
        }

        [HttpPost("CreateDevice")]
        public async Task<IActionResult> MakeAttendenceSys (string EmpId, string Device_Id , string DeviceName)
        {
           var DeviceAvail = _repository.GetDeviceInfo(Device_Id);

            if (!DeviceAvail)
            {
                Device dev = new Device();
                dev.Device_Id = Device_Id;
                dev.DeviceName = DeviceName;
                _repository.InsertDevice(dev);
            }

            string Tdate = DateTime.Now.ToString("dd/MM/yyyy");
            var IsAttendence = _repository.CheckAttendenceEmp(EmpId, Tdate);
            AttendenceReport attend = new AttendenceReport();
            if (IsAttendence == null)
            {
                attend.InTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm").Split(" ")[1];
                attend.OutTime = "";
                attend.EmpId = EmpId;
                attend.Device_Id = Device_Id;
                attend.date = Tdate;
                _repository.InsertAttendenceReport(attend);
            }
            else
            {
                if (IsAttendence.OutTime == "") 
                {
                IsAttendence.OutTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm").Split(" ")[1];
                _repository.AttendenceUpdate(IsAttendence);
                }
                else
                {
                    return NotFound("OutTime Marked Already");
                }
            }
            //device.dateTimeHour = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            //_repository.InsertDevice(device);
                return Ok("Success");
        }

        [HttpGet("GetDepartMent")]
        public IActionResult GetAllDepartment()
        {
            var response = this._repository.GetDepartment();
            return Ok(response);
        }

        [HttpGet("GetDesignation")]
        public IActionResult GetAllDesignation()
        { 
            var response = this._repository.GetDesignation();
            return Ok(response);
        }

        [HttpPost("InsertBasicDetails")]
        public async Task<IActionResult> InsertBasicDetails(EmpProfile empP)
        {
            var IsEmpAvail = _repository.EmpAvail(empP);
            if (IsEmpAvail!=null)
            {
                _repository.Update(IsEmpAvail);
            }
            else
            {
                _repository.InsertEmpProfile(empP);
            }
            return Ok("Success");
        }

        [HttpGet("GetDeviceDetails")]
        public IActionResult GetDevice()
        {
            var response = this._repository.GetDevice();
            return Ok(response);
        }

        [HttpGet("GetUserByEmpId")]
        public IActionResult GetDevicebyEmp(string empId)
        {
            var response = this._repository.GetEmpById(empId);
            //if (!response)
            //{
            //    var checkin = this._repository.GetcheckinTime(empId);
            //    return Ok(checkin);
            //}
            return Ok(response);
        }

        [HttpGet("AttendenceDetails")]
        public IActionResult GetAttendenceReport()
        {
            var response = this._repository.GetAttendeceReport();
            return Ok(response);
        }

        [HttpGet("GetAttendance")]
        public IActionResult GetAttendance()
        {
            var response = this._repository.GetAttendance();
            var checkin = response.FirstOrDefault().check_in.ToString().Split(" ");
            var date = checkin[0].Trim();
            var Time = checkin[1].Trim();
            return Ok(response);
        }
    }
}
