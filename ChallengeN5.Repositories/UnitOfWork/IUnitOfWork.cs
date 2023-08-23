
namespace ChallengeN5.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPermissionRepository PermissionRepository { get; }
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
