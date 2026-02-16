using Dashboard.Application.Interfaces;
using Dashboard.Application.Services;
using Dashboard.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers
{
    [Route("api/queues")]
    public class QueueController : ApiBaseController
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetQueues(
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
            var result = await _queueService.GetQueuesAsync(filter);
            return HandleResult(result);
        }
    }
}
