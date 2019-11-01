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
        Task CreateAsync(User user, CancellationToken cancellationToken);
        Task<User> Get(Guid id, CancellationToken cancellationToken);
        Task<User> Get(String username, String password, CancellationToken cancellationToken);

    }
}
