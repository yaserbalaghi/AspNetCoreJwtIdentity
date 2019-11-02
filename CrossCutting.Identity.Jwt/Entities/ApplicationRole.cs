using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class ApplicationRole
    { 
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        private readonly List<ApplicationUserRoles> _users;
        public virtual ICollection<ApplicationUserRoles> Users => _users.AsReadOnly();

        public ApplicationRole()
        {
            _users = new List<ApplicationUserRoles>();
        }
    }
}
