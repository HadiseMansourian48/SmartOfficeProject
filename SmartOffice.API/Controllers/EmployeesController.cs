using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.Application.DTOs.Employees;
using SmartOffice.Application.Interfaces.Employees;
using SmartOffice.Domain.Constants;

namespace SmartOffice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
    [Authorize(Policy = PermissionNames.Employees.View)]
    public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }
    [HttpPost]
    [Authorize(Policy = PermissionNames.Employees.Create)]
    public async Task<IActionResult> Create(CreateEmployeeRequest request)
    {
        var employee = await _employeeService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetAll),
            new { id = employee.Id },
            employee
            );
    }
    }

