namespace LMSweb.Models;

public partial class Mission
{
    public string Mid { get; set; } = null!;

    public string Mname { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public string CurrentAction { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Execution> Executions { get; set; } = new List<Execution>();

    public virtual Provided? Provided { get; set; }

    public virtual ICollection<ExecutionContent> ExecutionContents { get; set; } = new List<ExecutionContent>();
    public virtual ICollection<EvaluationCoaching> EvaluationCoachings { get; set; } = new List<EvaluationCoaching>();
}
