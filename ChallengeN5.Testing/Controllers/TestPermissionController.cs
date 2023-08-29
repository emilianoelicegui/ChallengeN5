using ChallengeN5.Api.Controllers;
using ChallengeN5.Repositories.Dto;
using ChallengeN5.Services.Services;
using ChallengeN5.Testing.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;

namespace ChallengeN5.Testing.Controllers
{
    public class TestPermissionController
    {
        
        [Fact]
        public async Task GetAllAsync()
        {
            // Arrange
            var permissionService = new Mock<IPermissionService>();
            var loggerPermissionService = new Mock<ILogger<PermissionController>>();
            permissionService.Setup(_ => _.GetAll()).Returns(PermissionMockData.GetAllDtoAsync());
            var permissionController = new PermissionController(loggerPermissionService.Object, permissionService.Object);

            // Act
            var result = (OkObjectResult)await permissionController.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<PermissionDto>>(result.Value);
        }

        [Fact]
        public async Task RequestAsync()
        {
            // Arrange
            var mockPermissionService = new Mock<IPermissionService>();
            var loggerPermissionService = new Mock<ILogger<PermissionController>>();
            mockPermissionService.Setup(service => service.Request(It.IsAny<int>()))
                                 .ReturnsAsync(PermissionMockData.GetAllDtoAsync().Result.First());
            var permissionController = new PermissionController(loggerPermissionService.Object, mockPermissionService.Object);

            // Act
            var result = await permissionController.Request(1);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task ModifyAsync()
        {
            // Arrange
            var mockPermissionService = new Mock<IPermissionService>();
            var loggerPermissionService = new Mock<ILogger<PermissionController>>();
            mockPermissionService.Setup(service => service.Modify(It.IsAny<ModifyPermissionDto>()))
                                 .Returns(Task.CompletedTask);

            var permissionController = new PermissionController(loggerPermissionService.Object, mockPermissionService.Object);

            // Act
            var result = await permissionController.Modify(PermissionMockData.NewRequest());

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
