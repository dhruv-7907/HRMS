using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notifications
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }  //Default 0
        public DateTime CreateAt { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employees Employees { get; set; }    
    }
}
