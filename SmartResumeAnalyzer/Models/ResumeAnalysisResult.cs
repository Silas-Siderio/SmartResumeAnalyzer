namespace SmartResumeAnalyzer.Models
{
    public class ResumeAnalysisResult
    {
        public string Summary { get; set; }
        public List<string> Strengths { get; set; }
        public List<string> Weaknesses { get; set; }
        public List<string> Suggestions { get; set; }
    }
}