using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Domain.Entities
{
    public class Department
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool IsActive { get; set; }
    }
}
