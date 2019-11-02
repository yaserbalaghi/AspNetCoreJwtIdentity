using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Entities;

namespace CrossCutting.Identity.Jwt.Contracts
{
    public interface IJwtIdentityRepository
    {
        Task CreateAsync(ApplicationUser user, CancellationToken cancellationToken);
        Task<ApplicationUser> Get(Guid id, CancellationToken cancellationToken);
        Task<ApplicationUser> Get(String username, String password, CancellationToken cancellationToken);
        Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken);
    }
}
