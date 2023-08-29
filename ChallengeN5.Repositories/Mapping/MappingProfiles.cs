using AutoMapper;
using ChallengeN5.Repositories.Dto;
using ChallengeN5.Repositories.Models;

namespace ChallengeN5.Repositories.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ModifyPermissionDto, Permission>();

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();

            CreateMap<PermissionType, PermissionTypeDto>();
            CreateMap<PermissionTypeDto, PermissionType>();
        }
    }
}
