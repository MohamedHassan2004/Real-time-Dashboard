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
        private readonly ICallRepository _callRepository;
        private readonly ILogger<StatsService> _logger;

        public StatsService(ICallRepository callRepository, ILogger<StatsService> logger)
        {
            _callRepository = callRepository;
            _logger = logger;
        }

        public async Task<Result<StatsDTO>> GetStatsAsync()
        {
            var calls = await _callRepository.GetAllAsync();
            var callList = calls.ToList();

            var answered = callList.Count(c => c.Status == CallStatus.Answered);
            var abandoned = callList.Count(c => c.Status == CallStatus.Abandoned);
            var inQueue = callList.Count(c => c.Status == CallStatus.InQueue);
            var totalOffered = callList.Count;

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
