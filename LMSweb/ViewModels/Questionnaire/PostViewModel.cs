namespace LMSweb.ViewModels.Questionnaire
{
    public class PostViewModel
    {
        public string UID { get; set; }
        public string MissionId { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public string? QuestionId { get; set; }
        public AnswerContent Content { get; set; }
    }

    public class AnswerContent
    {
        public string? OptionId { get; set; }
        public string? OcontentContent { get; set; }
    }
}
