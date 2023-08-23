using AutoMapper;
using ChallengeN5.Repositories.Dto;
using ChallengeN5.Repositories.Models;
using ChallengeN5.Repositories.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ChallengeN5.Services.Services.Impl
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(IUnitOfWork unitOfWork,
                IMapper mapper,
                ILogger<PermissionService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PermissionDto>> GetAll()
        {
            _logger.LogInformation("GetAll service ..");

            return _mapper.Map<IEnumerable<PermissionDto>>
                (await _unitOfWork.PermissionRepository.GetAllAsync());
        }

        public async Task Modify(PermissionDto permissionDto)
        {
            _logger.LogInformation("Modify service ..");

            Expression<Func<Permission, bool>> f = c => true;
            var permission = await _unitOfWork.PermissionRepository
                .GetAsync(expression: f = f.And(x => x.Id == permissionDto.Id));

            if (permission == null)
                throw new ValidationException("El permiso no existe");

            _mapper.Map(permissionDto, permission);

            await _unitOfWork.CommitAsync();
        }

        public async Task Request(PermissionDto permissionDto)
        {
            _logger.LogInformation("Request service ..");

            Expression<Func<Permission, bool>> f = c => true;

            var permission = _unitOfWork.PermissionRepository
                .Get(expression: f = f.And(x => x.NombreEmpleado.ToLower() == permissionDto.NombreEmpleado.ToLower()
                    && x.ApellidoEmpleado.ToLower() == permissionDto.ApellidoEmpleado.ToLower()
                    && x.TipoPermiso == permissionDto.TipoPermiso));

            if (permission != null)
                throw new ValidationException("Permiso ya existente");

            _unitOfWork.PermissionRepository.Add(_mapper.Map<Permission>(permissionDto));

            await _unitOfWork.CommitAsync();
        }
    }
}
