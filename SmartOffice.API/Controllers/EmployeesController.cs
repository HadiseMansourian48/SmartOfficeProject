using Microsoft.AspNetCore.Mvc;
using SmartOffice.Application.DTOs.Employees;
using SmartOffice.Application.Interfaces.Employees;

namespace SmartOffice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }
    [HttpPost]
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

