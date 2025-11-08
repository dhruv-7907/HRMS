using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateOnly Date {  get; set; }
        public bool Status { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employees Employees { get; set; }
    }
}
