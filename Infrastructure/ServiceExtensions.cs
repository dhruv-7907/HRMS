using Application.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDesignations, DesignationsRepository>();
            services.AddScoped<IDepartment, DepartmentRepository>();
            services.AddScoped<IEmployee, EmployeeRepository>();
            services.AddScoped<IFileService, FileServiceRepository>();
            services.Configure<FileStorageSettings>(
                 configuration.GetSection("FileStorageSettings"));
            return services;

        }
    }
}
