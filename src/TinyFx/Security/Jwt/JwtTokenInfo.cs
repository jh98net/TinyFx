using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace TinyFx.Security
{
    public class JwtTokenInfo
    {
        public JwtTokenStatus Status { get; set; } = JwtTokenStatus.Invalid;
        public string UserId { get; set; }
        public UserRole Role { get; set; }
        public string RoleString { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? IssuedAt { get; set; }
        public string UserIp { get; set; }
        [JsonIgnore]
        public ClaimsPrincipal Principal { get;set;}
    }
}
