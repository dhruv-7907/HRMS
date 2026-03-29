using Application.Interfaces;
using Infrastructure.HelperServices;
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasherService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IRefreshToken, RefreshTokenService>();
            services.Configure<FileStorageSettings>(
                 configuration.GetSection("FileStorageSettings"));
            return services;

        }
    }
}
