using ChallengeN5.Repositories.Models;

namespace ChallengeN5.Repositories
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Task<IEnumerable<Permission>> GetAllIncludeAsync(CancellationToken cancellationToken = default);
    }
}

