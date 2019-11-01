using System;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class User
    {
        public Guid Id { get; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FullName { get; set; }
        public Boolean Gender { get; set; }
        public String SecurityStamp { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}
