using Dashboard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.DTOs
{
    public class AgentDTO
    {
        public string Name { get; set; } = string.Empty;
        public AgentStatus Status { get; set; }
        public string Duration { get; set; } = string.Empty;

    }
}
