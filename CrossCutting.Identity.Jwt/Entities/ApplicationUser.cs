using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class ApplicationUser 
    {
        public Guid Id { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String FullName { get; set; }
        public String SecurityStamp { get; set; }
        public DateTime? LastLoginDate { get; set; } 

        private readonly List<ApplicationUserRoles> _roles;
        public virtual ICollection<ApplicationUserRoles> Roles => _roles.AsReadOnly();

        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
            _roles = new List<ApplicationUserRoles>();
        }
    }
}
