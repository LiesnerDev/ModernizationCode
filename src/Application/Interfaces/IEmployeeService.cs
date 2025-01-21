using System.Threading.Tasks;
using Employee.Application.Users.Models;

namespace Employee.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeModelResponse> AddAsync(EmployeeRequest employeeRequest);
        Task<EmployeeModelResponse> GetAsync(int id);
        Task<IEnumerable<EmployeeModelResponse>> GetAsync();
    }
}