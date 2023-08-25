using AutoMapper;
using ChallengeN5.Repositories.Dto;
using ChallengeN5.Repositories.Models;
using ChallengeN5.Repositories.UnitOfWork;
using Microsoft.Extensions.Logging;
using Nest;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ChallengeN5.Services.Services.Impl
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionService> _logger;
        private readonly IElasticClient _elasticClient;
        private readonly IKafkaService _kafkaService;

        private string _elasticIndex = "permissions";

        public PermissionService(IUnitOfWork unitOfWork,
                IMapper mapper,
                ILogger<PermissionService> logger,
                IElasticClient elasticClient,
                IKafkaService kafkaService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _elasticClient = elasticClient;
            _kafkaService = kafkaService;
        }

        public async Task<IEnumerable<PermissionDto>> GetAll()
        {
            _logger.LogInformation("GetAll service ..");

            var permissionsElastic = _elasticClient.Search<Permission>(s => s
                .MatchAll()
            );

            //message in kafka
            _kafkaService.ProduceOperationMessage(new KafkaMessageDto
            {
                Id = Guid.NewGuid(),
                OperationName = "get"
            });

            if (!permissionsElastic.Documents.Any())
                return _mapper.Map<IEnumerable<PermissionDto>>
                    (await _unitOfWork.PermissionRepository.GetAllIncludeAsync());

            return _mapper.Map<IEnumerable<PermissionDto>>
                    (permissionsElastic.Documents);
        }

        public async Task Modify(UpsertPermissionDto permissionDto, int idPermission)
        {
            try
            {
                _logger.LogInformation("Modify service ..");

                //message in kafka
                _kafkaService.ProduceOperationMessage(new KafkaMessageDto
                {
                    Id = Guid.NewGuid(),
                    OperationName = "modify"
                });

                Expression<Func<Permission, bool>> f = c => true;
                var permission = await _unitOfWork.PermissionRepository
                    .GetAsync(expression: f = f.And(x => x.Id == idPermission));

                if (permission == null)
                    throw new ValidationException("El permiso no existe");

                //sql
                _mapper.Map(permissionDto, permission);

                //elastic 
                var updateResponse = _elasticClient.Update<Permission, object>(idPermission, u => u
                    .Doc(permission)
                    .DocAsUpsert()
                );

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }           
        }

        public async Task Request(UpsertPermissionDto permissionDto)
        {
            try
            {
                _logger.LogInformation("Request service ..");

                //message in kafka
                _kafkaService.ProduceOperationMessage(new KafkaMessageDto
                {
                    Id = Guid.NewGuid(),
                    OperationName = "request"
                });

                Expression<Func<Permission, bool>> f = c => true;

                var permission = _unitOfWork.PermissionRepository
                    .Get(expression: f = f.And(x => x.NombreEmpleado.ToLower() == permissionDto.NombreEmpleado.ToLower()
                        && x.ApellidoEmpleado.ToLower() == permissionDto.ApellidoEmpleado.ToLower()
                        && x.TipoPermiso == permissionDto.TipoPermiso));

                if (permission != null)
                    throw new ValidationException("Permiso ya existente");

                permission = _mapper.Map<Permission>(permissionDto);
                
                //sql
                _unitOfWork.PermissionRepository.Add(permission);

                //elastic
                var indexRequest = new IndexRequest<Permission>
                    (permission, _elasticIndex, Guid.NewGuid().ToString());
                var indexResponse = await _elasticClient.IndexAsync(indexRequest);

                await _unitOfWork.CommitAsync();
            }
            catch 
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
