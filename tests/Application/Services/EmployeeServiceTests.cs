using System.Threading.Tasks;
using Moq;
using Xunit;
using Employee.Application.Interfaces;
using Employee.Application.Services;
using Employee.Application.Users.Models;
using Employee.Domain.Entities;
using Employee.Domain.SeedWork;

namespace Employee.Tests.Application.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<INotification> _notificationMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _notificationMock = new Mock<INotification>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _notificationMock.Object);
        }

        [Fact]
        public async Task AddAsync_ValidEmployee_AddsEmployeeAndReturnsResponse()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                Id = 1234,
                Name = "John Doe",
                Age = 30,
                Address = "123 Main Street"
            };

            _employeeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeService.AddAsync(employeeRequest);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Employee>(e => e.EmployeeId == employeeRequest.Id &&
                                                                                           e.Name == employeeRequest.Name &&
                                                                                           e.Age == employeeRequest.Age &&
                                                                                           e.Address == employeeRequest.Address)), Times.Once);
            _notificationMock.Verify(n => n.AddNotification(It.IsAny<string>()), Times.Never);
            Assert.NotNull(result);
            Assert.Equal(employeeRequest.Id, result.Id);
            Assert.Equal(employeeRequest.Name, result.Name);
            Assert.Equal(employeeRequest.Age, result.Age);
            Assert.Equal(employeeRequest.Address, result.Address);
        }

        [Theory]
        [InlineData(123, "John Doe", 30, "123 Main Street")] // Invalid ID
        [InlineData(12345, "John Doe", 30, "123 Main Street")] // Invalid ID
        [InlineData(1234, "Johnathan Doe the Second", 30, "123 Main Street")] // Invalid Name
        [InlineData(1234, "John Doe", 9, "123 Main Street")] // Invalid Age
        [InlineData(1234, "John Doe", 100, "123 Main Street")] // Invalid Age
        [InlineData(1234, "John Doe", 30, "123 Main Street, Apartment 456, Building 7, Some Very Long Address Exceeding Thirty Characters")]
        public async Task AddAsync_InvalidEmployee_AddsNotificationAndDoesNotAddEmployee(int id, string name, int age, string address)
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                Id = id,
                Name = name,
                Age = age,
                Address = address
            };

            // Act
            var result = await _employeeService.AddAsync(employeeRequest);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Employee>()), Times.Never);
            _notificationMock.Verify(n => n.AddNotification("Invalid employee data."), Times.Once);
            Assert.Null(result);
        }
    }
}