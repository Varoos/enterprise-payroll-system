using Enterprise.Payroll.Domain.Common;
using Enterprise.Payroll.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Payroll.Domain.Entities
{
    public class Employee: AuditableEntity, ISoftDeletable
    {
        public Guid Id { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Department Department { get; set; }
        public EmploymentType EmploymentType { get; set; }

        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}