using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LeaveRequests
    {
        public int Id { get; set; }
        public int LeaveType { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int Status { get; set; }

        public DateTime AppliedOn { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employees Employees { get; set; }

        [ForeignKey("ApprovedBy")]
        public virtual Users Users { get; set; }  // HR/Manager (UserId reference)

    }
}
