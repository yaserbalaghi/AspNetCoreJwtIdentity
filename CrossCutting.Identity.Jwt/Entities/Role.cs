using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class Role
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        private readonly List<UserRoles> _users;
        public virtual ICollection<UserRoles> Users => _users.AsReadOnly();

        public Role()
        {
            _users = new List<UserRoles>();
        }
    }
}
