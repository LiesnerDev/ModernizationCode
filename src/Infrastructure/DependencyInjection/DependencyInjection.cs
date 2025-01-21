using Microsoft.Extensions.DependencyInjection;
using Employee.Application.Interfaces;
using Employee.Application.Services;
using Employee.Infrastructure.Repository;

namespace Employee.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }
    }
}