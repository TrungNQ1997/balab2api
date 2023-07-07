using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.DTO
{
    public class LoginResultDTO
    {
        [AllowNull]
        public string? Username { get; set; }
        [AllowNull]
        public int? UserId { get; set; }
        [AllowNull]
        public string? Token { get; set; }
        [AllowNull]
        
        public DateTime? ExpiredDate { get; set; }
    }
}
