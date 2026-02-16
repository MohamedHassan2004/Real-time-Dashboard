using Microsoft.AspNetCore.SignalR;

namespace Dashboard.API.Hubs
{
    public class DashboardHub : Hub
    {
        // Clients only receive data — no client-to-server methods needed.
        // Events sent to clients:
        //   "ReceiveStats"  → StatsDTO
        //   "ReceiveAgents" → List<AgentDTO>
        //   "ReceiveQueues" → List<QueueDTO>
    }
}
