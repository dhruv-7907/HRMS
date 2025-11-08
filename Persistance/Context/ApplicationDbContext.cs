using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
          
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Designations> Designations { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<LeaveRequests> LeaveRequests { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<SalarySlips> SalarySlips { get; set; }
        public DbSet<SalaryStructure> SalaryStructures { get; set; }
        public DbSet<Deparments> Deparments { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
