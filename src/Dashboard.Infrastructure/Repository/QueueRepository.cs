using Dashboard.Domain.Common;
using Dashboard.Domain.Entities;
using Dashboard.Domain.Interfaces;
using Dashboard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Infrastructure.Repository
{
    public class QueueRepository : GenericRepository<Domain.Entities.Queue>, IQueueRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISieveProcessor _sieveProcessor;

        public QueueRepository(ApplicationDbContext context, ISieveProcessor sieveProcessor)
            : base(context, sieveProcessor)
        {
            _context = context;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedResult<Domain.Entities.Queue>> GetQueuesWithCallsPagedAsync(PaginationFilter filter)
        {
            var sieveModel = new SieveModel
            {
                Filters = filter.Filters,
                Sorts = filter.Sorts,
                Page = filter.Page,
                PageSize = filter.PageSize
            };

            IQueryable<Domain.Entities.Queue> query = _context.Queues
                .Include("_queueCalls")
                .AsNoTracking();

            var filteredQuery = _sieveProcessor.Apply(sieveModel, query, applyPagination: false);
            var totalCount = await filteredQuery.CountAsync();

            var pagedQuery = _sieveProcessor.Apply(sieveModel, filteredQuery, applyFiltering: false, applySorting: false);
            var items = await pagedQuery.ToListAsync();

            return new PagedResult<Domain.Entities.Queue>
            {
                Items = items,
                TotalCount = totalCount,
                Page = sieveModel.Page ?? 1,
                PageSize = sieveModel.PageSize ?? 10
            };
        }
    }
}

