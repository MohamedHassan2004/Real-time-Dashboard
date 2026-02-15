namespace Dashboard.Application.DTOs
{
    public class StatsDTO
    {
        public double SlaPercentage { get; set; }
        public int TotalOffered { get; set; }
        public int Answered { get; set; }
        public int Abandoned { get; set; }
        public int InQueue { get; set; }
    }
}
