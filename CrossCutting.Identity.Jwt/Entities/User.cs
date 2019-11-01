using System;

namespace CrossCutting.Identity.Jwt.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FullName { get; set; }
        public Boolean Gender { get; set; }
    }
}
