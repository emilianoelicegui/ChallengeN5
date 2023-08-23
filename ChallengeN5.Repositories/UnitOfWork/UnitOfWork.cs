using ChallengeN5.Repositories.Contexts;
using ChallengeN5.Repositories.Repositories.Impl;

namespace ChallengeN5.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly N5DbContext _dbContext;
        private IPermissionRepository _permissionRepository;

        public UnitOfWork(N5DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IPermissionRepository PermissionRepository
        {
            get { return _permissionRepository = _permissionRepository ?? new PermissionRepository(_dbContext); }
        }

        public void Commit()
            => _dbContext.SaveChanges();

        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();

        public void Rollback()
            => _dbContext.Dispose();

        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}
