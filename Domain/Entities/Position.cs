using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Domain.Entities
{
    public class Position
    {
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
