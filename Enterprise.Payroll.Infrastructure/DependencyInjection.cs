using Enterprise.Payroll.Application.Interfaces;
using Enterprise.Payroll.Infrastructure.Data;
using Enterprise.Payroll.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Payroll.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
