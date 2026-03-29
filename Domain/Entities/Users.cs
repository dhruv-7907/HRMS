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

            public string Name { get; set; } = default!;

            public required string Email { get; set; } 

            public string Password { get; set; } = default!;

            public int RoleId { get; set; }

            [ForeignKey(nameof(RoleId))]
            public virtual Roles Roles { get; set; } = default!;

            public bool IsActive { get; set; } = true;

            public bool IsEmailVerified { get; set; } = false;

            public int FailedLoginAttempts { get; set; } = 0;

            public DateTime? LockoutEnd { get; set; }

            public DateTime? LastLoginAt { get; set; }

           
        }

    
}
