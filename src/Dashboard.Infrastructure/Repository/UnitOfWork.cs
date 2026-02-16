using Dashboard.Application.Interfaces;
using Dashboard.Domain.Entities;
using Dashboard.Domain.Interfaces;
using Dashboard.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAgentRepository Agents { get; private set; }
        public IQueueRepository Queues { get; private set; }
        public ICallRepository Calls { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context,
            ICallRepository callRepository,
            IAgentRepository agentRepository,
            IQueueRepository queueRepository)
        {
            _context = context;
            Calls = callRepository;
            Agents = agentRepository;
            Queues = queueRepository;
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
