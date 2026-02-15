using Dashboard.Application.Interfaces;
using Dashboard.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers
{
    [Route("api/agents")]
    public class AgentController : ApiBaseController
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAgents(
            [FromQuery] string? filters,
            [FromQuery] string? sorts,
            [FromQuery] int? page,
            [FromQuery] int? pageSize)
        {
            var filter = new PaginationFilter
            {
                Filters = filters,
                Sorts = sorts,
                Page = page,
                PageSize = pageSize
            };
            var result = await _agentService.GetAgentsAsync(filter);
            return HandleResult(result);
        }
    }
}
