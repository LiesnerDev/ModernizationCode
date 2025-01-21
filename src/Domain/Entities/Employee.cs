using Employee.Domain.SeedWork;

namespace Employee.Domain.Entities
{
    public class Employee : Entity
    {
        public int EmployeeId { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Address { get; private set; }

        public Employee(int employeeId, string name, int age, string address)
        {
            EmployeeId = employeeId;
            Name = name;
            Age = age;
            Address = address;
        }
    }
}