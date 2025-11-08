using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payroll
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Decimal GrossSalary { get; set; }
        public Decimal NetSalary { get; set; }
        public DateTime GeneratedOn { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employees Employees { get; set; }

    }
}
