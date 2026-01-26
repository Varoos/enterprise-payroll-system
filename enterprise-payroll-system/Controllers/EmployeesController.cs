using Enterprise.Payroll.Application.Interfaces;
using Enterprise.Payroll.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Enterprise.Payroll.API.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var result = await _employeeService.CreateAsync(employee);
            return Ok(result);
        }
    }
}
