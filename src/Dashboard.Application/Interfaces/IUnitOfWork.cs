using Dashboard.Domain.Entities;
using Dashboard.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAgentRepository Agents { get; }
        IQueueRepository Queues { get; }
        ICallRepository Calls { get; }

        Task<int> CompleteAsync();
    }
}
