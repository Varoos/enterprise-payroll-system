using Enterprise.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise.Payroll.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmployeeCode)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(e => e.Salary)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
