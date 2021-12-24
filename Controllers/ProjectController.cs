using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker_API.Models.DTOs;
using ProjectTracker_API.Services.Abstract;
using System.Threading.Tasks;
using TeamExpenseAPI.Helpers;

namespace ProjectTracker_API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _entityService;

        public ProjectController(IProjectService entityService)
        {
            _entityService = entityService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody()] ProjectDTO projectDTO)
        {
            var result = await _entityService.CreateProject(projectDTO, ContextHelper.GetUserID(HttpContext));
            return Ok(result);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> Get(int projectId)
        {
            var result = await _entityService.GetProject(projectId, ContextHelper.GetUserID(HttpContext));
            return Ok(result);
        }

        [HttpPost("/ProjectUpdate")]
        public async Task<IActionResult> Update([FromBody()] ProjectDTO projectDTO, int projectId)
        {
            var result = await _entityService.UpdateProject(projectDTO, projectId, ContextHelper.GetUserID(HttpContext));
            return Ok(result);
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> Delete(int projectId)
        {
            var result = await _entityService.DeleteProject(projectId, ContextHelper.GetUserID(HttpContext));
            return Ok(result);
        }

    }
}
