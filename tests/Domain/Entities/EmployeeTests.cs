using Xunit;
using Employee.Domain.Entities;

namespace Employee.Tests.Domain.Entities
{
    public class EmployeeTests
    {
        [Fact]
        public void Employee_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int id = 1234;
            string name = "Jane Doe";
            int age = 28;
            string address = "456 Elm Street";

            // Act
            var employee = new Employee(id, name, age, address);

            // Assert
            Assert.Equal(id, employee.EmployeeId);
            Assert.Equal(name, employee.Name);
            Assert.Equal(age, employee.Age);
            Assert.Equal(address, employee.Address);
        }
    }
}