using Enterprise.Payroll.Application.Interfaces;
using Enterprise.Payroll.Domain.Entities;
using Enterprise.Payroll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .Where(e => e.IsActive)
            .ToListAsync();
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        employee.DateOfJoining = DateTime.SpecifyKind(
                employee.DateOfJoining,
                DateTimeKind.Utc
            );

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
}
