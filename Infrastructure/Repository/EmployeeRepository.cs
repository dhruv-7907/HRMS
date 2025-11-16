using Application.Interfaces;
using Application.ModelDto.Request;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly IApplicationDbContext _context;

        public EmployeeRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(EmployeeDto employeeDto)
        {
            var Employee = new Employees
            {
                Name = employeeDto.Name,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                DOB = employeeDto.DOB,
                Gender = employeeDto.Gender,
                ContactNumber = employeeDto.ContactNumber,
                Email = employeeDto.Email,
                Address = employeeDto.Address,
                JoinDate = employeeDto.JoinDate,
                ProfileImage = employeeDto.ProfileImage ?? string.Empty,
                Status = employeeDto.Status,
                DesignationId = (int)employeeDto.DesignationId
            };
            _context.Employees.Add(Employee);
            var result = await _context.SaveChangesAsync();
            return result;

        }

        public async Task<int> Delete(int Id)
        {
            _context.Employees.Remove(new Employees { Id = Id});
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ApiResponse<PagedResponse<EmployeeDto>>> GetAll(PaginationParams pagination)
        {
            IQueryable<Employees> query = _context.Employees.AsNoTracking();

            if (!string.IsNullOrEmpty(pagination.Name))
            {
                query = query.Where(d => d.Name.Contains(pagination.Name));
            }

            var totalRecords = await query.CountAsync();

            var items = await query.OrderBy(d => d.Id).Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).Select(d => new EmployeeDto
            {
                Name = d.Name,
                FirstName = d.FirstName,
                LastName = d.LastName,
                DOB = d.DOB,
                Gender = d.Gender,
                ContactNumber = d.ContactNumber,
                Email = d.Email,
                Address = d.Address,
                JoinDate = d.JoinDate,
                ProfileImage = d.ProfileImage ?? string.Empty,
                Status = d.Status,
                DesignationId = (int)d.DesignationId
            }).ToListAsync();

            var pagedResponse = new PagedResponse<EmployeeDto>(
                      items,
                     pagination.PageNumber,
                     pagination.PageSize,
                    totalRecords
               );
            return new ApiResponse<PagedResponse<EmployeeDto>>(pagedResponse);
        }

        public async Task<EmployeeDto?> GetById(int id)
        {
            return await _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Gender = e.Gender,
                    DOB = e.DOB,
                    ContactNumber = e.ContactNumber,
                    Email = e.Email,
                    Address = e.Address,
                    JoinDate = e.JoinDate,
                    ProfileImage = e.ProfileImage,
                    Status = e.Status,
                    DesignationId = e.DesignationId
                })
                .FirstOrDefaultAsync();

            //var employee = await _context.Employees.FindAsync(id);
            //return employee == null ? null : new EmployeeDto(employee);
        }

        public async Task<int> Update(EmployeeDto employeeDto)
        {
            // Load employee by ID
            var employee = await _context.Employees.FindAsync(employeeDto.Id);

            if (employee == null)
                throw new Exception("Employee not found");

            // Update fields
            employee.Name = employeeDto.Name;
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.DOB = employeeDto.DOB;
            employee.Gender = employeeDto.Gender;
            employee.ContactNumber = employeeDto.ContactNumber;
            employee.Email = employeeDto.Email;
            employee.Address = employeeDto.Address;
            employee.JoinDate = employeeDto.JoinDate;
            employee.ProfileImage = employeeDto.ProfileImage ?? employee.ProfileImage; // keep old image if null
            employee.Status = employeeDto.Status;
            employee.DesignationId = employeeDto.DesignationId ?? employee.DesignationId;

            _context.Employees.Update(employee);
            // Save
            var result = await _context.SaveChangesAsync();
            return result;
        }

    }
}
