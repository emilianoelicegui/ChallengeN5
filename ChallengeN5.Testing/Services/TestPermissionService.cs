using AutoMapper;
using ChallengeN5.Repositories;
using ChallengeN5.Repositories.Dto;
using ChallengeN5.Repositories.Mapping;
using ChallengeN5.Repositories.Models;
using ChallengeN5.Repositories.UnitOfWork;
using ChallengeN5.Services.Services;
using ChallengeN5.Services.Services.Impl;
using ChallengeN5.Testing.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Nest;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ChallengeN5.Testing.Services
{
    public class TestPermissionService
    {
        private readonly IMapper _mapper;

        public TestPermissionService()
        {
            // Configura el mock de IMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles()); 
                cfg.CreateMap<Permission, PermissionDto>();
            });

            _mapper = configuration.CreateMapper();
        }

        #region GetAll

        [Fact]
        public async Task GetAll_Results_Sql_Ok()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
                mockMapper.Setup(mapper => mapper.Map<IEnumerable<PermissionDto>>(It.IsAny<IEnumerable<Permission>>()))
                      .Returns<IEnumerable<Permission>>(source => _mapper.Map<IEnumerable<PermissionDto>>(source));

            var mockLogger = new Mock<ILogger<PermissionService>>();
            var mockKafka = new Mock<IKafkaService>();

            var mockElasticClient = new Mock<IElasticClient>();
                mockElasticClient
                .Setup(client => client.SearchAsync<Permission>(
                    It.IsAny<Func<SearchDescriptor<Permission>, ISearchRequest>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Mock<ISearchResponse<Permission>>().Object); 

            var mockPermissionRepository = new Mock<IPermissionRepository>();
            mockPermissionRepository.Setup(repo => repo.GetAllIncludeAsync(It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(await PermissionMockData.GetAllAsync()); // Configura los datos que deseas devolver

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.PermissionRepository)
                          .Returns(mockPermissionRepository.Object);

            var sut = new PermissionService(
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockLogger.Object,
                mockElasticClient.Object,
                mockKafka.Object
                );

            // Act
            var result = (List<PermissionDto>)await sut.GetAll();

            // Assert
            Assert.IsType<List<PermissionDto>>(result);
            Assert.True(result.Any());
            
        }

        [Fact]
        public async Task GetAll_Results_Elastic_Ok()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<PermissionDto>>(It.IsAny<IEnumerable<Permission>>()))
                  .Returns<IEnumerable<Permission>>(source => _mapper.Map<IEnumerable<PermissionDto>>(source));

            var mockLogger = new Mock<ILogger<PermissionService>>();
            var mockKafka = new Mock<IKafkaService>();

            var mockElasticClient = new Mock<IElasticClient>();
            mockElasticClient
            .Setup(client => client.SearchAsync<Permission>(
                It.IsAny<Func<SearchDescriptor<Permission>, ISearchRequest>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Mock<ISearchResponse<Permission>>().Object);

            var responseMock = new Mock<ISearchResponse<Permission>>();
            responseMock.SetupGet(r => r.ApiCall.HttpStatusCode).Returns(200);
            responseMock.SetupGet(r => r.Documents).Returns((IReadOnlyCollection<Permission>)await PermissionMockData.GetAllAsync());

            mockElasticClient.Setup(client => client.Search<Permission>(It.IsAny<Func<SearchDescriptor<Permission>, ISearchRequest>>()))
                            .Returns(responseMock.Object);

            var mockPermissionRepository = new Mock<IPermissionRepository>();
            mockPermissionRepository.Setup(repo => repo.GetAllIncludeAsync(It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(await PermissionMockData.GetAllAsync()); // Configura los datos que deseas devolver

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.PermissionRepository)
                          .Returns(mockPermissionRepository.Object);

            var sut = new PermissionService(
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockLogger.Object,
                mockElasticClient.Object,
                mockKafka.Object
                );

            // Act
            var result = (List<PermissionDto>)await sut.GetAll();

            // Assert
            Assert.IsType<List<PermissionDto>>(result);
            Assert.True(result.Any());
        }

        [Fact]
        public async Task GetAll_Results_NotFound()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<PermissionDto>>(It.IsAny<IEnumerable<Permission>>()))
                  .Returns<IEnumerable<Permission>>(source => _mapper.Map<IEnumerable<PermissionDto>>(source));

            var mockLogger = new Mock<ILogger<PermissionService>>();
            var mockKafka = new Mock<IKafkaService>();

            var mockElasticClient = new Mock<IElasticClient>();
            mockElasticClient
            .Setup(client => client.SearchAsync<Permission>(
                It.IsAny<Func<SearchDescriptor<Permission>, ISearchRequest>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Mock<ISearchResponse<Permission>>().Object);

            var mockPermissionRepository = new Mock<IPermissionRepository>();
            mockPermissionRepository.Setup(repo => repo.GetAllIncludeAsync(It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(await PermissionMockData.GetEmptyAsync()); 

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.PermissionRepository)
                          .Returns(mockPermissionRepository.Object);

            var service = new PermissionService(
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockLogger.Object,
                mockElasticClient.Object,
                mockKafka.Object
                );

            // Act
            var result = (List<PermissionDto>)await service.GetAll();

            // Assert
            Assert.IsType<List<PermissionDto>>(result);
            Assert.True(!result.Any());

        }

        #endregion GetAll

        #region Request

        [Fact]
        public async Task Request_Results_Ok()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<PermissionService>>();
            var mockElasticClient = new Mock<IElasticClient>();
            var mockKafkaService = new Mock<IKafkaService>();

            // Configuración del mock del PermissionRepository
            var mockPermissionRepository = new Mock<IPermissionRepository>();
            mockPermissionRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Permission, bool>>>()))
                                   .Returns((Permission)null); // Simula que el permiso no existe

            mockUnitOfWork.Setup(uow => uow.PermissionRepository)
                          .Returns(mockPermissionRepository.Object);

            var service = new PermissionService(
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockLogger.Object,
                mockElasticClient.Object,
                mockKafkaService.Object
            );

            // Act
            await service.Request(1);

            // Assert
            // Verifica que el método ProduceOperationMessage del mock de KafkaService haya sido llamado
            mockKafkaService.Verify(kafkaService => kafkaService.ProduceOperationMessage(It.IsAny<KafkaMessageDto>()), Times.Once);

            // Verifica que el método Add del mock del PermissionRepository haya sido llamado
            mockPermissionRepository.Verify(repo => repo.Add(It.IsAny<Permission>()), Times.Once);

            // Verifica que el método CommitAsync del mock del UnitOfWork haya sido llamado
            mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Request_Results_Duplicated()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<PermissionService>>();
            var mockElasticClient = new Mock<IElasticClient>();
            var mockKafkaService = new Mock<IKafkaService>();

            // Configuración del mock del PermissionRepository
            var mockPermissionRepository = new Mock<IPermissionRepository>();
            mockPermissionRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Permission, bool>>>()))
                                   .Returns(new Permission()); // Simula que el permiso ya existe

            mockUnitOfWork.Setup(uow => uow.PermissionRepository)
                          .Returns(mockPermissionRepository.Object);

            var service = new PermissionService(
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockLogger.Object,
                mockElasticClient.Object,
                mockKafkaService.Object
            );

            // Act y Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.Request(1));
        }

        #endregion Request

        #region Modify

        [Fact]
        public async Task Modify_Results_Ok()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<PermissionService>>();
            var mockElasticClient = new Mock<IElasticClient>();
            var mockKafkaService = new Mock<IKafkaService>();

            // Configuración del mock del PermissionRepository
            var mockPermissionRepository = new Mock<IPermissionRepository>();
            mockPermissionRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Permission, bool>>>()))
                                   .Returns(new Permission()); // Simula que el permiso existe

            mockUnitOfWork.Setup(uow => uow.PermissionRepository)
                          .Returns(mockPermissionRepository.Object);

            var service = new PermissionService(
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockLogger.Object,
                mockElasticClient.Object,
                mockKafkaService.Object
            );

            // Act
            await service.Modify(PermissionMockData.NewRequest());

            // Assert
            // Verifica que el método ProduceOperationMessage del mock de KafkaService haya sido llamado
            mockKafkaService.Verify(kafkaService => kafkaService.ProduceOperationMessage(It.IsAny<KafkaMessageDto>()), Times.Once);

            // Verifica que el método Update del mock de ElasticClient haya sido llamado
            mockElasticClient.Verify(
                elasticClient => elasticClient.Update<Permission, object>(
                    It.IsAny<DocumentPath<Permission>>(), It.IsAny<Func<UpdateDescriptor<Permission, object>, IUpdateRequest<Permission, object>>>()), Times.Once);

            // Verifica que el método CommitAsync del mock del UnitOfWork haya sido llamado
            mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Modify_Results_NotFound()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<PermissionService>>();
            var mockElasticClient = new Mock<IElasticClient>();
            var mockKafkaService = new Mock<IKafkaService>();

            // Configuración del mock del PermissionRepository
            var mockPermissionRepository = new Mock<IPermissionRepository>();
            mockPermissionRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Permission, bool>>>()))
                                   .Returns((Permission)null); // Simula que el permiso no existe

            mockUnitOfWork.Setup(uow => uow.PermissionRepository)
                          .Returns(mockPermissionRepository.Object);

            var service = new PermissionService(
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockLogger.Object,
                mockElasticClient.Object,
                mockKafkaService.Object
            );

            // Act y Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.Modify(PermissionMockData.NewRequest()));
        }
    }

    #endregion Modify
}
