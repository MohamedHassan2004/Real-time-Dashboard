using Dashboard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Domain.Entities
{
    public class Queue
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        private readonly List<Call> _queueCalls = new();
        public IReadOnlyList<Call> GetQueueCalls() => _queueCalls.AsReadOnly();

        public Queue() { }

        public Queue(string name)
        {
            Name = name;
        }

        public void AddCall(Call call)
        {
            if (call.QueueId != Id)
                throw new InvalidOperationException("Call's QueueId does not match this Queue's Id.");
            _queueCalls.Add(call);
        }

        public void ChangeCallStatus(int callId, CallStatus newStatus)
        {
            var call = _queueCalls.Find(c => c.Id == callId);
            if (call == null)
                throw new InvalidOperationException("Call not found in this Queue.");
            call.Status = newStatus;
        }
    }
}
