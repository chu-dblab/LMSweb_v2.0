namespace LMSweb.ViewModels.Evaluation
{
    public class EvaluationShowViewModel
    {
        public string? CourseId { get; set; }
        public string? MissionId { get; set; }

        public string? MissionName { get; set; }

        public string? CodeUrl { get; set; }
        public string? DrawUrl { get; set; }

        public EvaPE? EvaPE { get; set; }
        public List<EvaAnswer> EvaAnswer { get; set; } = new List<EvaAnswer>();
    }

    // 這邊是存回饋簡答題 題目與答案
    public class EvaAnswer
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
    }

    // Peer Evaluation -> PE 這邊要存放的是該資料的分數
    public class EvaPE
    {
        public string? PE01 { get; set; }
        public string? PE02 { get; set; }
        public string? PE03 { get; set; }
    }
}
