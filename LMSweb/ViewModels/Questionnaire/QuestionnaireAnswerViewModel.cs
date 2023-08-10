namespace LMSweb.ViewModels.Questionnaire
{
    public class QuestionnaireAnswerViewModel
    {
        public string CourseId { get; set; }
        public string MissionId { get; set; }

        public string MissionName { get; set; }
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
