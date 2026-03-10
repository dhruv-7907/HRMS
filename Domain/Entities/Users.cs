using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Users: BaseEntity
    {
      public int Id { get; set; }
      public required string Name { get; set; }
      public required string Password { get; set; }
      public required string Email { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Roles Roles { get; set; }

    }
}
