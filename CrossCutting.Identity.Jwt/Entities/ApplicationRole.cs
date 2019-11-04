using System;
using Microsoft.AspNetCore.Identity;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public String Description { get; set; }
    }
}
