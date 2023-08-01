namespace LMSweb.ViewModels.Questionnaire
{
    public class PostViewModel
    {
        string UID { get; set; }
        List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        string? QuestionId { get; set; }
        AnswerContent Content { get; set; }
    }

    public class AnswerContent
    {
        string? OptionId { get; set; }
        string? OcontentContent { get; set; }
    }
}
