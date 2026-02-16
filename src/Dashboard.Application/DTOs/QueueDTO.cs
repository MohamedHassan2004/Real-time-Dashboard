using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.DTOs
{
    public class QueueDTO
    {
        public string Name { get; set; } = string.Empty;
        public int InQueue { get; set; }
        public string MaxWait { get; set; } = string.Empty;
        public string? OldestCallCreatedAt { get; set; }
    }
}
