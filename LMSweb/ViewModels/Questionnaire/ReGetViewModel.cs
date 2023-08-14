namespace LMSweb.ViewModels.Questionnaire
{
    public class ReGetViewModel
    {
        public Topic Topic { get; set; } = null!;
        public Evaluation? Evaluation { get; set; 
        }
    }

    public class Topic
    {
        public string Name { get; set; } = null!;
        public List<Question> Questions { get; set; } = null!;
    }

    public class Question
    {
        public string QuestionId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Type { get; set; } = null!;
        public List<Option> Options { get; set; } = null!;
    }

    public class Option
    {
        public string? OptionId { get; set; }
        public string Content { get; set; } = null!;
    }

    public class Evaluation
    {
        public string GroupId { get; set; } = null!;
        public string GroupLeaderId { get; set; } = null!;
        public string DrawingUrl { get; set; } = null!;
        public string CodingUrl { get; set; } = null!;
    }
}
