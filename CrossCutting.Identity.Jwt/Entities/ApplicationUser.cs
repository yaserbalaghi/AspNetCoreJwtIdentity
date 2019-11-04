using System;
using Microsoft.AspNetCore.Identity;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public String FullName { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
