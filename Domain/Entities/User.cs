using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
