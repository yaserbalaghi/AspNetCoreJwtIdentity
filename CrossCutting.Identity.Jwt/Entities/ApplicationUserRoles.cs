using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class ApplicationUserRoles
    {
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Int32 RoleId { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}
