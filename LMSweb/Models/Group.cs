namespace LMSweb.Models;

public partial class Group
{
    public int Gid { get; set; }

    public string CourseId { get; set; }

    public string Gname { get; set; } = null!;

    public virtual ICollection<Execution> Executions { get; set; } = new List<Execution>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<ExecutionContent> ExecutionContents { get; set; } = new List<ExecutionContent>();

    public virtual Course Course { get; set; }
}
