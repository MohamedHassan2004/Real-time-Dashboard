using Dashboard.Domain.Entities;
using Dashboard.Domain.Interfaces;
using Dashboard.Infrastructure.Data;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Infrastructure.Repository
{
    public class CallRepository : GenericRepository<Call>, ICallRepository
    {
        public CallRepository(ApplicationDbContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor)
        {

        }
    }
}