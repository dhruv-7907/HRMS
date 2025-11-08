using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class SalaryStructure
    {
        public int Id { get; set; }
        public Decimal BasicSalary { get; set; }
        public Decimal HRA {  get; set; }
        public Decimal Allowances { get; set; }
        public Decimal Deductions { get; set; }
        public DateOnly EffectiveForm { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees Employees { get; set; }

    }
}
