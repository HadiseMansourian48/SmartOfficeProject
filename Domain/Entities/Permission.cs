using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Domain.Entities
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
