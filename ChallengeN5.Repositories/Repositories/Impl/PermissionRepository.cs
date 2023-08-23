using ChallengeN5.Repositories.Contexts;
using ChallengeN5.Repositories.Models;

namespace ChallengeN5.Repositories.Repositories.Impl
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(N5DbContext context) : base(context)
        { }
    }
}
