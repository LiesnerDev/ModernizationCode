using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using Employee.Domain.Entities;
using Employee.Infrastructure.Persistence;
using Employee.Infrastructure.Repository;

namespace Employee.Tests.Infrastructure.Repository
{
    public class EmployeeRepositoryTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeTestDb")
                .Options;
            _contextMock = new Mock<ApplicationDbContext>(options);
            _employeeRepository = new EmployeeRepository(_contextMock.Object);
        }

        [Fact]
        public async Task AddAsync_AddsEmployeeToDatabase()
        {
            // Arrange
            var employee = new Employee(1234, "Jane Doe", 28, "456 Elm Street");

            // Act
            await _employeeRepository.AddAsync(employee);

            // Assert
            _contextMock.Verify(c => c.Employees.AddAsync(employee, default), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsEmployee()
        {
            // Arrange
            int employeeId = 1234;
            var employee = new Employee(employeeId, "Jane Doe", 28, "456 Elm Street");
            _contextMock.Setup(c => c.Employees.FindAsync(employeeId)).ReturnsAsync(employee);

            // Act
            var result = await _employeeRepository.GetByIdAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employeeId, result.EmployeeId);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            int employeeId = 9999;
            _contextMock.Setup(c => c.Employees.FindAsync(employeeId)).ReturnsAsync((Employee)null);

            // Act
            var result = await _employeeRepository.GetByIdAsync(employeeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee(1234, "Jane Doe", 28, "456 Elm Street"),
                new Employee(5678, "John Smith", 35, "789 Maple Avenue")
            };
            _contextMock.Setup(c => c.Employees.ToListAsync(default)).ReturnsAsync(employees);

            // Act
            var result = await _employeeRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                e => {
                    Assert.Equal(1234, e.EmployeeId);
                    Assert.Equal("Jane Doe", e.Name);
                },
                e => {
                    Assert.Equal(5678, e.EmployeeId);
                    Assert.Equal("John Smith", e.Name);
                });
        }
    }
}