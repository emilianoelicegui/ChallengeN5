using ChallengeN5.Repositories.Contexts;
using ChallengeN5.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeN5.Repositories.Repositories.Impl
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(N5DbContext context) : base(context)
        { 
        
        }

        public async Task<IEnumerable<Permission>> GetAllIncludeAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Permissions.Include(x => x.TipoPermisoNavigation).ToListAsync();
        }
    }
}
