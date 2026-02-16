using Dashboard.Application.Interfaces;
using Dashboard.Domain.Enums;
using System.Linq;

namespace Dashboard.API.Services
{
    public class DashboardBroadcastService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DashboardBroadcastService> _logger;
        private readonly Random _random = new();

        public DashboardBroadcastService(IServiceProvider serviceProvider, ILogger<DashboardBroadcastService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var notifier = scope.ServiceProvider.GetRequiredService<IDashboardNotifier>();
                    var statsService = scope.ServiceProvider.GetRequiredService<IStatsService>();
                    var agentService = scope.ServiceProvider.GetRequiredService<IAgentService>();
                    var queueService = scope.ServiceProvider.GetRequiredService<IQueueService>();

                    // 1. Simulate agent status changes
                    var agents = await unitOfWork.Agents.GetAllAsync();
                    var agentList = agents.ToList();
                    var randomAgent = agentList.ElementAtOrDefault(_random.Next(agentList.Count));
                    if (randomAgent != null)
                    {
                        var newStatus = (AgentStatus)_random.Next(0, 3); // 0=Online, 1=Idle, 2=NotReady
                        randomAgent.UpdateStatus(newStatus);
                        unitOfWork.Agents.Update(randomAgent);
                    }

                    // 2. Simulate new incoming calls (30% chance each cycle)
                    if (_random.Next(1, 10) > 7)
                    {
                        await unitOfWork.Calls.AddAsync(new Domain.Entities.Call
                        {
                            QueueId = _random.Next(1, 3),
                            Status = CallStatus.InQueue,
                            CreatedAt = DateTime.UtcNow
                        });
                    }

                    // 3. Process existing InQueue calls (simulate answering/abandoning)
                    var inQueueCalls = await unitOfWork.Calls.FindAsync(c => c.Status == CallStatus.InQueue);
                    foreach (var call in inQueueCalls)
                    {
                        if (_random.NextDouble() < 0.3) // 30% chance to change status
                        {
                            if (_random.NextDouble() < 0.8) // 80% Answered, 20% Abandoned
                            {
                                // Assign to a random agent
                                var randomAgentToAssign = agentList.ElementAt(_random.Next(agentList.Count));
                                call.AssignAgent(randomAgentToAssign.Id);
                            }
                            else
                            {
                                call.UpdateStatus(CallStatus.Abandoned);
                            }
                            unitOfWork.Calls.Update(call);
                        }
                    }

                    await unitOfWork.CompleteAsync();

                    // 3. Fetch latest data and broadcast via SignalR
                    var statsResult = await statsService.GetStatsAsync();
                    if (statsResult.IsSuccess)
                        await notifier.SendStatsUpdate(statsResult.Value);

                    var agentsResult = await agentService.GetAgentsAsync(new Domain.Common.PaginationFilter { PageSize = 100 });
                    if (agentsResult.IsSuccess)
                        await notifier.SendAgentsUpdate(agentsResult.Value.Items);

                    var queuesResult = await queueService.GetQueuesAsync(new Domain.Common.PaginationFilter { PageSize = 100 });
                    if (queuesResult.IsSuccess)
                        await notifier.SendQueuesUpdate(queuesResult.Value.Items);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in dashboard broadcast cycle");
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}