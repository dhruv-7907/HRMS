using Application.Interfaces;
using Application.ModelDto.Responce;
using Domain.Common;
using Domain.Entities;
using MediatR;
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

        public async Task<ApiResponse<PagedResponse<DepartmentDto>>> GetAllDesignations(PaginationParams pagination)
        {
            //var query = _context.Designations.AsQueryable();
            IQueryable<Designations> query = _context.Designations.AsNoTracking();

            if (!string.IsNullOrEmpty(pagination.Name))
            {
                query = query.Where(d => d.Name.Contains(pagination.Name));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.OrderBy(d => d.Id).Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();

            var pagedResponse = new PagedResponse<DepartmentDto>(
                      items,
                     pagination.PageNumber,
                     pagination.PageSize,
                    totalRecords
               );
            return new ApiResponse<PagedResponse<DepartmentDto>>(pagedResponse);
        }

        public async Task<DesignationsDto> GetDesignationsById(int Id)
        {
            var item = await _context.Designations
                                     .AsNoTracking()
                                     .Where(d => d.Id == Id)
                                     .Select(d => new DesignationsDto { Id = d.Id, Name = d.Name })
                                     .FirstOrDefaultAsync();
            return item;

        //    var entity = await _context.Designations
        //.AsNoTracking()
        //.FirstOrDefaultAsync(d => d.Id == id);

        //    if (entity == null)
        //        return null;

        //    return new DepartmentDto
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name
        //    };
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
