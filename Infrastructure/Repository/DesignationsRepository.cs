using Application.Interfaces;
using Application.ModelDto.Responce;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DesignationsRepository:IDesignations
    {
        private readonly IApplicationDbContext _context;

        public DesignationsRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateDesignations(DesignationsDto dto)
        {
            //Designations
            // Convert DTO → Entity
            var designations = new Designations
            {
                Name = dto.Name,
                DepartmentId = dto.DepartmentId
            };

            // Add to database
            _context.Designations.Add(designations);
            await _context.SaveChangesAsync();

            // Return newly created Id
            return designations.Id;
        }

        public async Task<int> DeleteDesignations(int Id)
        {
            var designation = await _context.Designations.FindAsync(Id);
            if (designation == null)
                return 0;
                _context.Designations.Remove(designation);
                await _context.SaveChangesAsync();
                return Id;
        }

        public async Task<int> UpdateDesignations(DesignationsDto dto)
        {
            var designation = await _context.Designations.FindAsync(dto.Id);

            if (designation == null)
                return 0; // Not found  

            // Update only existing record
            designation.Name = dto.Name;
            designation.DepartmentId = dto.DepartmentId;

            _context.Designations.Update(designation);
            await _context.SaveChangesAsync();

            return designation.Id;
        }

    }
}
