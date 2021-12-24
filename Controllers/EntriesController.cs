using EntryTracker.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker_API.Models.DTOs;
using System.Threading.Tasks;
using TeamExpenseAPI.Helpers;

namespace EntryTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryService _entryService;

        public EntriesController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody()] EntryDTO entryDTO)
        {
            var result = await _entryService.CreateEntry(entryDTO, ContextHelper.GetUserID(HttpContext));
            return Ok(result);
        }

        [HttpGet("{entryId}")]
        public async Task<IActionResult> Get(int entryId)
        {
            var result = await _entryService.GetEntry(entryId, ContextHelper.GetUserID(HttpContext));
            return Ok(result);
        }

        [HttpPost("/EntryUpdate")]
        public async Task<IActionResult> Update([FromBody()] EntryDTO entryDTO, int entryId)
        {
            var result = await _entryService.UpdateEntry(entryDTO, entryId, ContextHelper.GetUserID(HttpContext));
            return Ok(result);
        }

        [HttpDelete("{entryId}")]
        public async Task<IActionResult> Delete(int entryId)
        {
            var result = await _entryService.DeleteEntry(entryId, ContextHelper.GetUserID(HttpContext));
            return Ok(result);
        }

    }
}
