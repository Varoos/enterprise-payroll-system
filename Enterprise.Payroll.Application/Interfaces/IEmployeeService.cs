using Enterprise.Payroll.Application.DTOs.Employees;
using Enterprise.Payroll.Domain.Entities;

namespace Enterprise.Payroll.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> CreateAsync(Employee employee);
    }
}