using Dashboard.Application.DTOs;
using Dashboard.Domain.Common;

namespace Dashboard.Application.Interfaces
{
    public interface IStatsService
    {
        Task<Result<StatsDTO>> GetStatsAsync();
    }
}
