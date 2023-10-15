namespace LMSweb.ViewModels.Coaching
{
    public class CoachingShowViewModel
    {
        public string? CourseId { get; set; }
        public string? MissionId { get; set; }

        public string? MissionName { get; set; }

        public string? CodeUrl { get; set; }
        public string? DrawUrl { get; set; }

        public CoaPC? CoaPC { get; set; }
        public List<CoaAnswer> CoaAnswer { get; set; } = new List<CoaAnswer>();
    }

    // 這邊是存回饋簡答題 題目與答案
    public class CoaAnswer
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
    }

    // Peer Evaluation -> PE 這邊要存放的是該資料的分數
    public class CoaPC
    {
        public string? PC01 { get; set; }
        public string? PC02 { get; set; }
        public string? PC03 { get; set; }
    }
}
