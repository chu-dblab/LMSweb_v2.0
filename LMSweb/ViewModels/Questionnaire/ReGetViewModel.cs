namespace LMSweb.ViewModels.Questionnaire
{
    public class ReGetViewModel
    {
        public Topic Topic { get; set; } = null!;
        public Evaluation? Evaluation { get; set; }
        public Coaching? Coaching { get; set; }
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

    public class Coaching 
    {
        public CoachingScore ClassAgv { get; set; } = null!;
        public CoachingScore GroupAgv { get; set; } = null!;

        public List<CoachingGroup> Groups { get; set; } = null!;
    }

    public class CoachingScore
    {
        public double PE01 { get; set; }
        public double PE02 { get; set; }
        public double PE03 { get; set; }
    }

    public class CoachingGroup
    {
        public string GroupLeaderId { get; set; } = null!;
        public CoachingScore CoachingScore { get; set; } = null!;
        public List<string> Evaluation { get; set; } = null!;
    }
}
