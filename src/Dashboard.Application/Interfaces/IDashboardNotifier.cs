using Dashboard.Application.DTOs;

namespace Dashboard.Application.Interfaces
{
    public interface IDashboardNotifier
    {
        Task SendStatsUpdate(StatsDTO stats);
        Task SendAgentsUpdate(IEnumerable<AgentDTO> agents);
        Task SendQueuesUpdate(IEnumerable<QueueDTO> queues);
    }
}
