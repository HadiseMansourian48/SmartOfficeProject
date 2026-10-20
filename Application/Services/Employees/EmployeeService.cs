using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.EntityFrameworkCore;
using SmartOffice.Application.DTOs.Employees;
using SmartOffice.Application.Interfaces.Employees;
using SmartOffice.Domain.Entities;

namespace SmartOffice.Application.Services.Employees;
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<EmployeeDto>> GetAllAsync()
        {
        var employees = await _repository.GetAllAsync();
            return employees
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    EmployeeCode = x.EmployeeCode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NationalCode = x.NationalCode,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    HireDate = x.HireDate,
                    EmploymentStatus = x.EmploymentStatus,
                    DepartmentId = x.DepartmentId,
                    PositionId = x.PositionId

                })
                .ToList();
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            EmployeeCode = request.EmployeeCode,
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalCode = request.NationalCode,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            HireDate = request.HireDate,
            EmploymentStatus = request.EmploymentStatus,
            DepartmentId = request.DepartmentId,
            PositionId = request.PositionId
        };
        var createdEmployee = await _repository.AddAsync(employee);

        return new EmployeeDto
        { 
              Id = createdEmployee.Id,
              EmployeeCode= createdEmployee.EmployeeCode, 
              FirstName = createdEmployee.FirstName,
              LastName= createdEmployee.LastName,
              NationalCode= createdEmployee.NationalCode,
              Email= createdEmployee.Email,
              PhoneNumber= createdEmployee.PhoneNumber,
              HireDate= createdEmployee.HireDate,
              EmploymentStatus= createdEmployee.EmploymentStatus,
              DepartmentId= createdEmployee.DepartmentId,
              PositionId= createdEmployee.PositionId
        };


    }
}

