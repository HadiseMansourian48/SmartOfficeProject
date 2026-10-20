using System;
using System.Collections.Generic;
using System.Text;
using SmartOffice.Domain.Entities;
namespace SmartOffice.Application.Interfaces.Employees
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> AddAsync(Employee employee);
    }
}
