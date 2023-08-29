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

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await _permissionService.GetAll());
        }

        [HttpGet("Request/{id}")]
        public async Task<IActionResult> Request(int id)
        {
            return Ok(await _permissionService.Request(id));
        }

        [HttpPut("Modify")]
        public async Task<IActionResult> Modify([FromBody] ModifyPermissionDto permissionDto)
        {            
            await _permissionService.Modify(permissionDto);
            return Ok();
        }
    }
}
