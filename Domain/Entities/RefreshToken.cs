using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RefreshToken:BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Token { get; set; } = default!;

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? RevokedAt { get; set; }

        public string? ReplacedByToken { get; set; }

        public string? CreatedByIp { get; set; }

        [ForeignKey(nameof(UserId))]
        public Users Users { get; set; } = default!;
    }
}
