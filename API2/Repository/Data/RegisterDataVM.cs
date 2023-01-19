using System;

namespace API2.Repositories.Data
{
    public class RegisterDataVM
    {
        public string NIK { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public string UniversityName { get; set; }

    }
    public enum Gender
    {
        Male,
        Female
    }
}