using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Enterprise.Payroll.Domain.Enums;

namespace Enterprise.Payroll.Application.DTOs.Employees
{
    public class CreateEmployeeDto
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Department Department { get; set; }
        public EmploymentType EmploymentType { get; set; }

        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}