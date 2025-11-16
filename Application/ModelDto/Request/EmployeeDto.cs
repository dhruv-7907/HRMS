using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ModelDto.Request
{
    public class EmployeeDto
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Gender { get; set; }
        public DateOnly DOB { get; set; }
        public required string ContactNumber { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public DateTime JoinDate { get; set; }
        public string? ProfileImage { get; set; }
        public bool Status { get; set; }
        public int? DesignationId { get; set; }
    }
}
