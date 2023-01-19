using API2.Base;
using API2.Context;
using API2.Models;
using API2.Repositories.Data;
using API2.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API2.Controllers
{
    public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;      
        public EmployeeController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;

        }


        [HttpPost]
        [Route("Register")]
        [EnableCors("AllowOrigin")]
        public ActionResult Register (RegisterVM registerVM)
        {
            var Reg = employeeRepository.Register(registerVM);
            if (Reg == 1)
            {
                return StatusCode(200,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = "Data Berhasil Di Simpan",
                        Data = Reg
                    });
            }else if (Reg == 777)
            {
                return StatusCode(777,
                    new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Email Sudah Ada",
                        Data = Reg
                    });
            }else if (Reg == 888)
            {
                return StatusCode(888,
                    new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Nomor Phone Sudah Ada",
                        Data = Reg
                    });
            }else
            {
                return StatusCode(500,
                    new
                    {
                        status = HttpStatusCode.InternalServerError,
                        message= "Data Tidak Tersimpan",
                        Data = Reg
                    });
            }
            
        }

        [HttpGet]
        [Route("Register")]
        [EnableCors("AllowOrigin")]
        public ActionResult GetRegister()
        {
            var data = employeeRepository.GetRegData();

            return Ok(data);
        }
       


    }
}
