using AutoMapper;
using Dashboard.Application.DTOs;
using Dashboard.Application.Interfaces;
using Dashboard.Domain.Common;
using Dashboard.Domain.Enums;
using Dashboard.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Dashboard.Application.Services
{
    public class QueueService : IQueueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<QueueService> _logger;

        public QueueService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<QueueService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<PagedResult<QueueDTO>>> GetQueuesAsync(PaginationFilter filter)
        {
            var pagedQueues = await _unitOfWork.Queues.GetQueuesWithCallsPagedAsync(filter);

            if (pagedQueues == null)
            {
                _logger.LogWarning("Queues Not found");
                return Result.Failure<PagedResult<QueueDTO>>("NOT_FOUND", "No Queues found");
            }

            var queueDTOs = pagedQueues.Items.Select(q => MapQueue(q));

            _logger.LogInformation("Queues fetched successfully");
            return Result.Success<PagedResult<QueueDTO>>(
                new PagedResult<QueueDTO>
                {
                    Items = queueDTOs,
                    TotalCount = pagedQueues.TotalCount,
                    Page = pagedQueues.Page,
                    PageSize = pagedQueues.PageSize
                },
                "Queues fetched successfully");
        }

        // map helper
        private QueueDTO MapQueue(Domain.Entities.Queue queue)
        {
            var inQueueCalls = queue.GetQueueCalls();

            var maxWait = inQueueCalls.Count > 0
                ? DateTime.UtcNow - inQueueCalls.Min(c => c.CreatedAt)
                : TimeSpan.Zero;

            return new QueueDTO
            {
                Name = queue.Name,
                InQueue = inQueueCalls.Count,
                MaxWait = maxWait.ToString(@"hh\:mm\:ss"),
            };
        }

    }
}

