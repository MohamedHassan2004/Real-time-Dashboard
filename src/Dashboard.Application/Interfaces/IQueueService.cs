using Dashboard.Domain.Common;
using Dashboard.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Interfaces
{
    public interface IQueueService
    {
        Task<Result<PagedResult<QueueDTO>>> GetQueuesAsync(PaginationFilter filter);
    }
}
