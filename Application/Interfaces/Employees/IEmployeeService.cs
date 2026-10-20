using System;
using System.Collections.Generic;
using System.Text;
using SmartOffice.Application.DTOs.Employees;

namespace SmartOffice.Application.Interfaces.Employees
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> CreateAsync(CreateEmployeeRequest request);
    }
}
