using Dashboard.Domain.Common;
using Dashboard.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Domain.Interfaces
{
    public interface IQueueRepository : IGenericRepository<Domain.Entities.Queue>
    {
        Task<PagedResult<Domain.Entities.Queue>> GetQueuesWithCallsPagedAsync(PaginationFilter filter);
    }
}
