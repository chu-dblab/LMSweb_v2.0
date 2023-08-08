namespace LMSweb.Models;

public partial class ExperimentalProcedure
{
    public string EprocedureId { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
