using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Application.DTOs.Users
{
    public class UserDto
    {

        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public Guid? EmployeeId { get; set; }
        public string? EmployeeFullName { get; set; }
    }
}
