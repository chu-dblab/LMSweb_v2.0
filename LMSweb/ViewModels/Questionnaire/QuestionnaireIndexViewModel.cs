namespace LMSweb.ViewModels.Questionnaire
{
    public class QuestionnaireIndexViewModel
    {
        public string UID { get; set; }
        public string CourseId { get; set; }
        public string MissionId { get; set; }
        public string MissionName { get; set; }
        public string? TaskType { get; set; }
        public string? TaskSteps { get; set; }
        public string EprocedureId { get; set; }

        // 以下為互動式同儕互評所需欄位
        public List<string>? EvaluationGroupIdList { get; set; }

        public string? EvaluationGroupId { get; set; }
    }
}
