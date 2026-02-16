using Dashboard.Domain.Entities;
using Dashboard.Domain.Enums;
using Dashboard.Domain.Interfaces;
using Dashboard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Infrastructure.Repository
{
    public class CallRepository : GenericRepository<Call>, ICallRepository
    {
        private readonly ApplicationDbContext _context;
        public CallRepository(ApplicationDbContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor)
        {
            _context = context;
        }

        public async Task<(int TotalOffered, int Answered, int Abandoned, int InQueue)> GetTodayStatsAsync()
        {
            var todayUtc = DateTime.UtcNow.Date;

            var stats = await _context.Calls
                .Where(c => c.CreatedAt >= todayUtc)
                .GroupBy(_ => 1)
                .Select(g => new
                {
                    TotalOffered = g.Count(),
                    Answered = g.Count(c => c.Status == CallStatus.Answered),
                    Abandoned = g.Count(c => c.Status == CallStatus.Abandoned),
                    InQueue = g.Count(c => c.Status == CallStatus.InQueue)
                })
                .FirstOrDefaultAsync();

            return stats == null
                ? (0, 0, 0, 0)
                : (stats.TotalOffered, stats.Answered, stats.Abandoned, stats.InQueue);
        }
    }
}