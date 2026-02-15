using Dashboard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Dashboard.Domain.Entities
{
    public class Call
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CallStatus Status { get; set; } = CallStatus.InQueue;
        public int QueueId { get; set; }
        public virtual Queue Queue { get; set; } = null!;
        public int? AgentId { get; set; } = null;
        public virtual Agent? Agent { get; set; } = null;
        public DateTime? AnsweredAt { get; set; } = null;

        public Call() { }

        public Call(int queueId)
        {
            QueueId = queueId;
        }

        public void UpdateStatus(CallStatus newStatus)
        {
            if (!Enum.IsDefined<CallStatus>(newStatus))
            {
                throw new ArgumentException($"Invalid status value: {newStatus}", nameof(newStatus));
            }
            Status = newStatus;
        }

        public void AssignAgent(int agentId)
        {
            AgentId = agentId;
            Status = CallStatus.Answered;
            AnsweredAt = DateTime.UtcNow;
        }

    }
}
