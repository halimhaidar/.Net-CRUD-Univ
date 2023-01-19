using API2.Context;
using API2.Models;
using API2.Repository;
using API2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API2.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<RegisterDataVM> GetRegData()
        {
            var data2 = (from emp in myContext.Employee
                         join pro in myContext.Profilling on emp.NIK equals pro.NIK
                         join edu in myContext.Education on pro.Education_Id equals edu.Id
                         join univ in myContext.University on edu.University_Id equals univ.UniversityId
                         select new RegisterDataVM()
                         {
                             NIK = emp.NIK,
                             FullName = emp.FirstName + " " + emp.LastName,
                             Phone = emp.Phone,
                             Email = emp.Email,
                             Salary = emp.Salary,
                             Gender = (Gender)emp.Gender,
                             BirthDate = emp.BirthDate,
                             Degree = edu.Degree,
                             GPA = edu.GPA,
                             UniversityName = univ.Name
                         }).ToList();
            return data2;
        }
       
        public string GenerateNIK()
        {
            DateTime today = DateTime.Today;
            var dateNow = today.ToString("yyyyddMM");           
            var data = myContext.Employee.ToList();

            
            List<int> listId = new List<int>();
            for (int i = 0; i < data.Count(); i++)
            {
                var newId = data[i].NIK;
                if (newId.Length == 12)
                {
                    newId = newId.Substring(newId.Length - 4);                    
                    listId.Add(Convert.ToInt32(newId));
                }
            }

            int highestId = listId.Count() > 0 ? listId.Max() : 0;
            int increamentId = highestId + 1;
            string formatedNIK = increamentId.ToString().PadLeft(4, '0');
            formatedNIK = dateNow + formatedNIK;

            return formatedNIK;

        }

        public int Register(RegisterVM registerVM)
        {
            var checkEmail = myContext.Employee.FirstOrDefault(b => b.Email == registerVM.Email);
            var checkPhone = myContext.Employee.FirstOrDefault(b => b.Phone == registerVM.Phone);


            if (checkEmail != null)
            {
                return 777;

            }
            else if (checkPhone != null)
            {
                return 888;
            }
            else if (checkPhone == null && checkEmail == null)
            {
                var emp = new Employee();
                emp.NIK = GenerateNIK();
                emp.FirstName = registerVM.FirstName;
                emp.LastName = registerVM.LastName;
                emp.Phone = registerVM.Phone;
                emp.BirthDate = registerVM.BirtDate;
                emp.Salary = registerVM.Salary;
                emp.Email = registerVM.Email;
                emp.Gender = (Models.Gender)registerVM.Gender;
                myContext.Add(emp);
                myContext.SaveChanges();

                var acn = new Account();
                acn.NIK = emp.NIK;
                acn.Password = registerVM.Password;
                myContext.Add(acn);
                myContext.SaveChanges();

                var edu = new Education();
                edu.Degree = registerVM.Degree;
                edu.GPA = registerVM.GPA;
                edu.University_Id = registerVM.UniversityId;
                myContext.Add(edu);
                myContext.SaveChanges();

                var prof = new Profilling();
                prof.NIK = emp.NIK;
                prof.Education_Id = edu.Id;
                myContext.Add(prof);
                var result = myContext.SaveChanges();

                return result;
            }
            else
            {
                return 0;
            }
        }




    }
}
