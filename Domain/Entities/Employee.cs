using System;
using System.Collections.Generic;
using System.Text;
using SmartOffice.Domain.Enums;


namespace SmartOffice.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; } 
        public int EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        //public bool IsActive { get; set; }

        public EmploymentStatus EmploymentStatus { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }




    }
}
