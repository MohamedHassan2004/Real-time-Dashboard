using Dashboard.Domain.Entities;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace Dashboard.Infrastructure.Data
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options)
            : base(options) { }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            // Agent: allow filtering/sorting by Name and Status
            mapper.Property<Agent>(a => a.Name)
                .CanFilter()
                .CanSort();

            mapper.Property<Agent>(a => a.Status)
                .CanFilter()
                .CanSort();

            // Queue: allow filtering/sorting by Name
            mapper.Property<Queue>(q => q.Name)
                .CanFilter()
                .CanSort();

            return mapper;
        }
    }
}
