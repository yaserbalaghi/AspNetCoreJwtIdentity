using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class User
    {
        public Guid Id { get; protected set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FullName { get; set; }
        public Boolean Gender { get; set; }
        public String SecurityStamp { get; set; }

        private readonly List<UserRoles> _roles;
        public virtual ICollection<UserRoles> Roles => _roles.AsReadOnly();

        public User()
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
            _roles = new List<UserRoles>();
        }
    }
}
