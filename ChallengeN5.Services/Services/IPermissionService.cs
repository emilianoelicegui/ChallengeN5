using ChallengeN5.Repositories.Dto;
using ChallengeN5.Repositories.Models;

namespace ChallengeN5.Services.Services
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAll();
        Task Modify(UpsertPermissionDto permissionDto, int idPermission);
        Task Request(UpsertPermissionDto permissionDto);   
    }
}
