using Dashboard.Domain.Entities;
using Dashboard.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Call> Calls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // enums
            modelBuilder.Entity<Agent>()
                .Property(a => a.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Call>()
                .Property(c => c.Status)
                .HasConversion<int>();

            // indexes
            modelBuilder.Entity<Agent>()
                .HasIndex(a => a.Name);

            modelBuilder.Entity<Call>()
                .HasIndex(c => c.Status);

            modelBuilder.Entity<Call>()
                .HasIndex(c => c.CreatedAt);

            // relations 
            modelBuilder.Entity<Call>()
                .HasOne(c => c.Agent)
                .WithMany("_agentCalls")
                .HasForeignKey(c => c.AgentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Call>()
                .HasOne(c => c.Queue)
                .WithMany("_queueCalls")
                .HasForeignKey(c => c.QueueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            DataSeeder.Seed(modelBuilder);
        }
    }
}
