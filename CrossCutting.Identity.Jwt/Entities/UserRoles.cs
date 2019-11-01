using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class UserRoles
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Int32 RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
