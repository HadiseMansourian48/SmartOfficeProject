using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }  
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
