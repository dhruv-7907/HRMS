using Application.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDesignations, DesignationsRepository>();
            services.AddScoped<IDepartment, DepartmentRepository>();
            services.AddScoped<IEmployee, EmployeeRepository>();
            return services;

        }
    }
}
