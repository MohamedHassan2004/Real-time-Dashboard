using Dashboard.API.Hubs;
using Dashboard.Application.DTOs;
using Dashboard.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.API.SignalR
{
    public class SignalRDashboardNotifier : IDashboardNotifier
    {
        private readonly IHubContext<DashboardHub> _hubContext;

        public SignalRDashboardNotifier(IHubContext<DashboardHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendStatsUpdate(StatsDTO stats)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveStats", stats);
        }

        public async Task SendAgentsUpdate(IEnumerable<AgentDTO> agents)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveAgents", agents);
        }

        public async Task SendQueuesUpdate(IEnumerable<QueueDTO> queues)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveQueues", queues);
        }
    }
}
