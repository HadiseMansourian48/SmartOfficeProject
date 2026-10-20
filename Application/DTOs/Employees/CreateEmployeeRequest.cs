using SmartOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Application.DTOs.Employees
{
    public class CreateEmployeeRequest
    {
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
