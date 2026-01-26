using Enterprise.Payroll.Application.DTOs.Employees;
using Enterprise.Payroll.Application.Interfaces;
using Enterprise.Payroll.Domain.Entities;
using Enterprise.Payroll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Payroll.Infrastructure.Repositories
{
    public class EmployeeRepository 
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return await _context.Employees
                .Where(e => e.IsActive)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    EmployeeCode = e.EmployeeCode,
                    FullName = $"{e.FirstName} {e.LastName}",
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Department = e.Department,
                    EmploymentType = e.EmploymentType,
                    Salary = e.Salary,
                    IsActive = e.IsActive
                })
                .ToListAsync();
        }

        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return null;

            return new EmployeeDto
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FullName = $"{e.FirstName} {e.LastName}",
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Department = e.Department,
                EmploymentType = e.EmploymentType,
                Salary = e.Salary,
                IsActive = e.IsActive
            };
        }

        public async Task<Guid> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeCode = dto.EmployeeCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Department = dto.Department,
                EmploymentType = dto.EmploymentType,
                Salary = dto.Salary,
                DateOfJoining = dto.DateOfJoining,
                IsActive = true
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            employee.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
