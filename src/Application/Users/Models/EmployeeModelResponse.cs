using Employee.Domain.Entities;

namespace Employee.Application.Users.Models
{
    public class EmployeeModelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public static implicit operator EmployeeModelResponse(Employee employee)
        {
            if (employee == null) return null;
            return new EmployeeModelResponse
            {
                Id = employee.EmployeeId,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address
            };
        }
    }
}