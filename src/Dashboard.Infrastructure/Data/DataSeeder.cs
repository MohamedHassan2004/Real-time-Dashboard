using Dashboard.Domain.Entities;
using Dashboard.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var seedDate = new DateTime(2026, 2, 16, 8, 0, 0, DateTimeKind.Utc);

            // Agents
            modelBuilder.Entity<Agent>().HasData(
                new { Id = 1, Name = "Ahmed Hassan", Status = AgentStatus.Online, LastStatusChange = seedDate },
                new { Id = 2, Name = "Sara Mohamed", Status = AgentStatus.Online, LastStatusChange = seedDate.AddMinutes(15) },
                new { Id = 3, Name = "Omar Ali", Status = AgentStatus.Idle, LastStatusChange = seedDate.AddMinutes(30) },
                new { Id = 4, Name = "Nour Ibrahim", Status = AgentStatus.NotReady, LastStatusChange = seedDate.AddMinutes(45) },
                new { Id = 5, Name = "Youssef Khaled", Status = AgentStatus.Online, LastStatusChange = seedDate.AddMinutes(60) }
            );

            // Queues
            modelBuilder.Entity<Queue>().HasData(
                new { Id = 1, Name = "Technical Support" },
                new { Id = 2, Name = "Sales" },
                new { Id = 3, Name = "Billing" }
            );

            // Calls
            modelBuilder.Entity<Call>().HasData(
                // Answered calls
                new { Id = 1, QueueId = 1, Status = CallStatus.Answered, CreatedAt = seedDate.AddMinutes(5), AgentId = (int?)1, AnsweredAt = (DateTime?)seedDate.AddMinutes(6) },
                new { Id = 2, QueueId = 1, Status = CallStatus.Answered, CreatedAt = seedDate.AddMinutes(10), AgentId = (int?)2, AnsweredAt = (DateTime?)seedDate.AddMinutes(12) },
                new { Id = 3, QueueId = 2, Status = CallStatus.Answered, CreatedAt = seedDate.AddMinutes(20), AgentId = (int?)5, AnsweredAt = (DateTime?)seedDate.AddMinutes(21) },
                new { Id = 4, QueueId = 3, Status = CallStatus.Answered, CreatedAt = seedDate.AddMinutes(25), AgentId = (int?)1, AnsweredAt = (DateTime?)seedDate.AddMinutes(27) },
                new { Id = 5, QueueId = 2, Status = CallStatus.Answered, CreatedAt = seedDate.AddMinutes(35), AgentId = (int?)2, AnsweredAt = (DateTime?)seedDate.AddMinutes(36) },

                // Abandoned calls
                new { Id = 6, QueueId = 1, Status = CallStatus.Abandoned, CreatedAt = seedDate.AddMinutes(15), AgentId = (int?)null, AnsweredAt = (DateTime?)null },
                new { Id = 7, QueueId = 3, Status = CallStatus.Abandoned, CreatedAt = seedDate.AddMinutes(40), AgentId = (int?)null, AnsweredAt = (DateTime?)null },

                // Calls currently in queue
                new { Id = 8, QueueId = 1, Status = CallStatus.InQueue, CreatedAt = seedDate.AddMinutes(50), AgentId = (int?)null, AnsweredAt = (DateTime?)null },
                new { Id = 9, QueueId = 2, Status = CallStatus.InQueue, CreatedAt = seedDate.AddMinutes(55), AgentId = (int?)null, AnsweredAt = (DateTime?)null },
                new { Id = 10, QueueId = 3, Status = CallStatus.InQueue, CreatedAt = seedDate.AddMinutes(58), AgentId = (int?)null, AnsweredAt = (DateTime?)null }
            );
        }
    }
}
