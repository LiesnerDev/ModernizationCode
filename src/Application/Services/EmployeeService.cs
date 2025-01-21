using System.Threading.Tasks;
using Employee.Application.Interfaces;
using Employee.Application.Users.Models;
using Employee.Domain.Entities;
using Employee.Domain.SeedWork;

namespace Employee.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotification _notification;

        public EmployeeService(IEmployeeRepository employeeRepository, INotification notification)
        {
            _employeeRepository = employeeRepository;
            _notification = notification;
        }

        public async Task<EmployeeModelResponse> AddAsync(EmployeeRequest employeeRequest)
        {
            // Validation
            if (!ValidateEmployeeRequest(employeeRequest))
            {
                _notification.AddNotification("Invalid employee data.");
                return null;
            }

            var employee = new Employee(employeeRequest.Id, employeeRequest.Name, employeeRequest.Age, employeeRequest.Address);
            await _employeeRepository.AddAsync(employee);
            return employee;
        }

        public async Task<EmployeeModelResponse> GetAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee;
        }

        public async Task<IEnumerable<EmployeeModelResponse>> GetAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees;
        }

        private bool ValidateEmployeeRequest(EmployeeRequest request)
        {
            if (request.Id < 1000 || request.Id > 9999)
                return false;

            if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 20)
                return false;

            if (request.Age < 10 || request.Age > 99)
                return false;

            if (string.IsNullOrWhiteSpace(request.Address) || request.Address.Length > 30)
                return false;

            return true;
        }
    }
}