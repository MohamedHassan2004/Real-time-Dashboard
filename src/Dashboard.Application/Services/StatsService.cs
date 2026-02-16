using Dashboard.Application.DTOs;
using Dashboard.Application.Interfaces;
using Dashboard.Domain.Common;
using Dashboard.Domain.Enums;
using Dashboard.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dashboard.Application.Services
{
    public class StatsService : IStatsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StatsService> _logger;

        public StatsService(IUnitOfWork unitOfWork, ILogger<StatsService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<StatsDTO>> GetStatsAsync()
        {
            var (totalOffered, answered, abandoned, inQueue) = await _unitOfWork.Calls.GetTodayStatsAsync();

            var sla = totalOffered > 0
                ? Math.Round((double)answered / totalOffered * 100, 2)
                : 0;

            _logger.LogInformation("Stats fetched — SLA: {Sla}%, Total: {Total}, Answered: {Answered}, Abandoned: {Abandoned}, InQueue: {InQueue}",
                sla, totalOffered, answered, abandoned, inQueue);

            return Result.Success<StatsDTO>(new StatsDTO
            {
                SlaPercentage = sla,
                TotalOffered = totalOffered,
                Answered = answered,
                Abandoned = abandoned,
                InQueue = inQueue
            });
        }
    }
}

