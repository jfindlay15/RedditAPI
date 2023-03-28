namespace WebSockets.Helpers
{
    public struct MetricResult
    {
        public int MessageCount { get; set; }
        public List<string> TopTen { get; set; }
    }
}
