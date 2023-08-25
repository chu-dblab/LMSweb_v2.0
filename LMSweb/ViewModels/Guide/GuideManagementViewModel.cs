using LMSweb.ViewModels.Questionnaire;

namespace LMSweb.ViewModels.Guide
{
    public class GuideManagementViewModel
    {
        public string CourseId { get; set; }
        public string MissionId { get; set; }
        public string MissionName { get; set; }
        public List<GuideGroup> Groups { get; set; }
    }

    public class GuideGroup
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string? CurrentStatus { get; set; }
        public List<string>? StepsName { get; set; }

        // 後續要用可以用
        public List<GuideUser>? Users { get; set; }

        // 批改作業
        public QuestionnaireIndexViewModel? questionnaireIndexViewModel { get; set; }
    }

    public class GuideUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsLeader { get; set; }
    }
}
