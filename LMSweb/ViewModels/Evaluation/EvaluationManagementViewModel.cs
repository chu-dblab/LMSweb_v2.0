using LMSweb.ViewModels.Questionnaire;

namespace LMSweb.ViewModels.Evaluation
{
    public class EvaluationManagementViewModel
    {
        public string? CourseId { get; set; }
        public string? MissionId { get; set; }

        public string? TaskType { get; set; }
        public string? TaskSteps { get; set; }

        // 使否進入互動式同儕互評
        public bool IsPeerEvaluation { get; set; }
        public List<EvaGroup>? EvaGroupList { get; set; }
    }

    public class EvaGroup
    {
        public string GroupLeaderId { get; set; }
        public QuestionnaireIndexViewModel QuestionnaireIndexViewModel { get; set; }

        public bool IsSubmitted { get; set; }
        public bool IsAnswered { get; set; }
    }
}
