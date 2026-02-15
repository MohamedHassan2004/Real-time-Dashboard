using Dashboard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Domain.Entities
{
    public class Agent
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public AgentStatus Status { get; set; } = AgentStatus.NotReady;
        public DateTime LastStatusChange { get; private set; }

        private readonly List<Call> _agentCalls = new();
        public IReadOnlyCollection<Call> GetAgentCalls() => _agentCalls.AsReadOnly();


        public Agent() { }   

        public Agent(string name)
        {
            Name = name;
        } 

        public void UpdateStatus(AgentStatus newStatus)
        {
            if (!Enum.IsDefined<AgentStatus>(newStatus))
            {
                throw new ArgumentException($"Invalid status value: {newStatus}", nameof(newStatus));
            }

            if (Status != newStatus)
            {
                Status = newStatus;
                LastStatusChange = DateTime.UtcNow;
            }
        }
    }
}
