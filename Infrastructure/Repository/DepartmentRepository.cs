using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using DepartmentDtoRequest = Application.ModelDto.Request.DepartmentDto;
using DepartmentDtoResponce = Application.ModelDto.Responce.DepartmentDto;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DepartmentRepository : IDepartment
    {
        private readonly IApplicationDbContext _context;

        public DepartmentRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(DepartmentDtoRequest department)
        {
            var entity = new Deparments
            {
                Name = department.Name
            };

            await _context.Deparments.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public  async Task<int> Delete(int Id)
        {
            var entity = await _context.Deparments.FindAsync(Id);
            if (entity == null)
                return 0;
            _context.Deparments.Remove(entity);
            await _context.SaveChangesAsync();
            return Id;
        }

        public  async Task<ApiResponse<PagedResponse<DepartmentDtoResponce>>> GetAll(PaginationParams pagination)
        {
            IQueryable<Deparments> query = _context.Deparments.AsNoTracking();

            if (!string.IsNullOrEmpty(pagination.Name))
            {
                query = query.Where(d => d.Name.Contains(pagination.Name));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.OrderBy(d => d.Id).Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).Select(d => new DepartmentDtoResponce
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();

            var pagedResponse = new PagedResponse<DepartmentDtoResponce>(
                      items,
                     pagination.PageNumber,
                     pagination.PageSize,
                    totalRecords
               );
            return new ApiResponse<PagedResponse<DepartmentDtoResponce>>(pagedResponse);
        }

        public async Task<DepartmentDtoResponce> GetById(int Id)
        {
            var item = await _context.Deparments
                                   .AsNoTracking()
                                   .Where(d => d.Id == Id)
                                   .Select(d => new DepartmentDtoResponce { Id = d.Id, Name = d.Name })
                                   .FirstOrDefaultAsync();
            return item;
        }

        public async Task<int> Update(DepartmentDtoRequest department)
        {
            var deparments = await _context.Deparments.FindAsync(department.Id);

            if (deparments == null)
                return 0; // Not found  

            // Update only existing record
            deparments.Name = department.Name;

            _context.Deparments.Update(deparments);
            await _context.SaveChangesAsync();

            return department.Id;
        }
    }
}
