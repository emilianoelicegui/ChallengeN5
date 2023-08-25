using ChallengeN5.Repositories.Dto;
using ChallengeN5.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeN5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IPermissionService _permissionService;

        public PermissionController(ILogger<PermissionController> logger, IPermissionService permissionService)
        {
            _logger = logger;
            _permissionService = permissionService;
        }

        [HttpGet("GetPermissions")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _permissionService.GetAll());
        }

        [HttpPost("RequestPermission")]
        public async Task<IActionResult> RequestPermission([FromBody] UpsertPermissionDto permissionDto)
        {
            await _permissionService.Request(permissionDto);
            return Created("", permissionDto);
        }

        [HttpPut("ModifyPermission/{idPermission}")]
        public async Task<IActionResult> ModifyPermission([FromBody] UpsertPermissionDto permissionDto, int idPermission)
        {            
            await _permissionService.Modify(permissionDto, idPermission);
            return Ok();
        }
    }
}
