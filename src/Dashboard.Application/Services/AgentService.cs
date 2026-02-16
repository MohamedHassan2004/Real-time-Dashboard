using Dashboard.Domain.Common;
using Dashboard.Application.DTOs;
using Dashboard.Application.Interfaces;
using Dashboard.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Dashboard.Application.Services
{
    public class AgentService : IAgentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AgentService> _logger;

        public AgentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AgentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<PagedResult<AgentDTO>>> GetAgentsAsync(PaginationFilter filter)
        {
            var pagedAgents = await _unitOfWork.Agents.GetPagedAsync(filter);

            if (pagedAgents == null)
            {
                _logger.LogWarning("No Agents Found at {dateTime}", DateTime.UtcNow);
                return Result.Failure<PagedResult<AgentDTO>>("NOT_FOUND", "No agents found");
            }

            var agentDTOs = _mapper.Map<List<AgentDTO>>(pagedAgents.Items);

            _logger.LogInformation("Agents Fetched successfully at {dateTime}", DateTime.UtcNow);
            return Result.Success<PagedResult<AgentDTO>>(
                new PagedResult<AgentDTO>
                {
                    Items = agentDTOs,
                    TotalCount = pagedAgents.TotalCount,
                    Page = pagedAgents.Page,
                    PageSize = pagedAgents.PageSize
                },
                "Agents fetched successfully");
        }
    }
}

