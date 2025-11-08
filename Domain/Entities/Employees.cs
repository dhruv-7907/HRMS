using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateOnly DOB { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime JoinDate { get; set; }
        public string ProfileImage { get; set; }
        public bool Status { get; set; }

        public int DesignationId { get; set; }
        [ForeignKey(nameof(DesignationId))]
        public virtual Designations Designations { get; set; }

    }
}
