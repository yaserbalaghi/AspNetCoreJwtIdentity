using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Context;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrossCutting.Identity.Jwt.Repositories
{
    public class JwtIdentityRepository : IJwtIdentityRepository
    {
        private readonly IdentityDbContext _dbContext;

        public JwtIdentityRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        public async Task<User> Get(Guid id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

            return user;
        }

        public async Task<User> Get(String username, String password, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(u => u.UserName == username && u.Password == password, cancellationToken);
            
            return user;
        }

        public async Task UpdateAsync(User user,CancellationToken cancellationToken)
        {
             _dbContext.Users.Update(user);
             await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}