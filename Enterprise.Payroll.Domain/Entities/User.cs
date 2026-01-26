using Enterprise.Payroll.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Payroll.Domain.Entities
{
    public class User: AuditableEntity, ISoftDeletable
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}