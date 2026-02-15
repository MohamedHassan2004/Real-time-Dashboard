using Dashboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers
{
    [Route("api/stats")]
    public class StatsController : ApiBaseController
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var result = await _statsService.GetStatsAsync();
            return HandleResult(result);
        }
    }
}
