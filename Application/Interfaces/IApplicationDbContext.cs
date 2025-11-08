using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Deparments> Deparments { get; set; }
        DbSet<Designations> Designations { get; set; }
        DbSet<Users> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
