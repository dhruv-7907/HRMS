using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SalarySlips
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public DateTime SentOn { get; set; }
        [ForeignKey("PayRollId")]
        public virtual Payroll Payroll { get; set; }

    }
}
