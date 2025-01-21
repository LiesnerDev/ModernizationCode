using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Employee.API.Controllers;
using Employee.Application.Interfaces;
using Employee.Application.Users.Models;
using Employee.Domain.SeedWork;

namespace Employee.Tests.API.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly Mock<INotification> _notificationMock;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _notificationMock = new Mock<INotification>();
            _employeeController = new EmployeeController(_employeeServiceMock.Object, _notificationMock.Object);
        }

        [Fact]
        public async Task Post_ValidEmployee_ReturnsSuccessResponse()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                Id = 1234,
                Name = "Jane Doe",
                Age = 28,
                Address = "456 Elm Street"
            };
            var employeeResponse = new EmployeeModelResponse
            {
                Id = employeeRequest.Id,
                Name = employeeRequest.Name,
                Age = employeeRequest.Age,
                Address = employeeRequest.Address
            };
            _employeeServiceMock.Setup(service => service.AddAsync(It.IsAny<EmployeeRequest>())).ReturnsAsync(employeeResponse);

            // Act
            var result = await _employeeController.Post(employeeRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(employeeResponse, okResult.Value);
        }

        [Fact]
        public async Task Post_InvalidEmployee_ReturnsBadRequest()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                Id = 123, // Invalid ID
                Name = "Jane Doe",
                Age = 28,
                Address = "456 Elm Street"
            };
            _employeeServiceMock.Setup(service => service.AddAsync(It.IsAny<EmployeeRequest>())).ReturnsAsync((EmployeeModelResponse)null);

            // Act
            var result = await _employeeController.Post(employeeRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid employee data.", badRequestResult.Value);
        }

        [Fact]
        public async Task Get_ExistingEmployee_ReturnsEmployee()
        {
            // Arrange
            int employeeId = 1234;
            var employeeResponse = new EmployeeModelResponse
            {
                Id = employeeId,
                Name = "Jane Doe",
                Age = 28,
                Address = "456 Elm Street"
            };
            _employeeServiceMock.Setup(service => service.GetAsync(employeeId)).ReturnsAsync(employeeResponse);

            // Act
            var result = await _employeeController.Get(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(employeeResponse, okResult.Value);
        }

        [Fact]
        public async Task Get_NonExistingEmployee_ReturnsNotFound()
        {
            // Arrange
            int employeeId = 9999;
            _employeeServiceMock.Setup(service => service.GetAsync(employeeId)).ReturnsAsync((EmployeeModelResponse)null);

            // Act
            var result = await _employeeController.Get(employeeId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_AllEmployees_ReturnsListOfEmployees()
        {
            // Arrange
            var employees = new List<EmployeeModelResponse>
            {
                new EmployeeModelResponse { Id = 1234, Name = "Jane Doe", Age = 28, Address = "456 Elm Street" },
                new EmployeeModelResponse { Id = 5678, Name = "John Smith", Age = 35, Address = "789 Maple Avenue" }
            };
            _employeeServiceMock.Setup(service => service.GetAsync()).ReturnsAsync(employees);

            // Act
            var result = await _employeeController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(employees, okResult.Value);
        }
    }
}