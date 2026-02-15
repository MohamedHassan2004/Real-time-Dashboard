using Dashboard.Application.DTOs;
using Dashboard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Interfaces
{
    public interface IAgentService
    {
        Task<Result<PagedResult<AgentDTO>>> GetAgentsAsync(PaginationFilter filter);
    }
}
