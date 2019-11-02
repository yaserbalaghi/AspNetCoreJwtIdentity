using System;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Entities;

namespace CrossCutting.Identity.Jwt.Contracts
{
    public interface IJwtIdentityService
    {
        String GenerateToken(ApplicationUser user);
    }
}
