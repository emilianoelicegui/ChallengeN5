using ChallengeN5.Repositories.Dto;
using ChallengeN5.Repositories.Models;

namespace ChallengeN5.Services.Services
{
    public interface IPermissionService
    {
        Task <PermissionDto> Request(int? id);
        Task<IEnumerable<PermissionDto>> GetAll();
        Task Modify(ModifyPermissionDto permissionDto);   
    }
}
