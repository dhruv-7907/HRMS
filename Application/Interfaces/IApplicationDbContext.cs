using Application.ModelDto.Responce;
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
        DbSet<Designations> Designations { get; set; }
        DbSet<Deparments> Deparments { get; set; }
        DbSet<Users> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
