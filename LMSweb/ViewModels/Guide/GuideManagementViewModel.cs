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
        public string MissionId { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupLeaderId { get; set; }
        public string? CurrentStatus { get; set; }
        public List<string>? StepsName { get; set; }

        public bool IsCorrect { get; set; } = false;

        // 如果是平價組別需要呈現互評組別資訊
        public List<EvalustionGroup>? Evaluation { get; set; }

        public List<CoachingGroup>? Coaching { get; set; }

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

    public class EvalustionGroup
    {
        // 如果是平價組別需要呈現互評組別資訊
        public string EvaluationName { get; set; }

        /*
         * 這邊如果 LeaderId 有值代表該小組已經完成評價該組
         * 如果沒有 = null & EvaluationName 有值就代表該小組未完成評價該組
         */
        public string? EvalustionLeaderId { get; set; }
    }

    public class CoachingGroup
    {
        public string CoachingName { get; set; }
        public string? CoachingLeaderId { get; set; }
    }
}
