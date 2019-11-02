using System.Collections.Generic;
using System.Security.Claims;
using CrossCutting.Identity.Jwt.Entities;

namespace CrossCutting.Identity.Jwt.Contracts
{
    public interface IAdditionalClaims
    {
        IEnumerable<Claim> GetCustomClaims(ApplicationUser user); 
    }
}