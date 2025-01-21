using Microsoft.AspNetCore.Mvc;
using Employee.Application.Users.Models;
using Employee.Application.Interfaces;
using Employee.Domain.SeedWork;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, INotification notification) : base(notification)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeService.GetAsync();
            return Response(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _employeeService.GetAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Response(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeRequest employeeRequest)
        {
            var result = await _employeeService.AddAsync(employeeRequest);
            if (result == null)
            {
                return BadRequest("Invalid employee data.");
            }
            return Response(result);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // Implementation for updating employee can be added here.
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Implementation for deleting employee can be added here.
        }
    }
}