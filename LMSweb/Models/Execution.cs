namespace LMSweb.Models;

public partial class Execution
{
    public int GroupId { get; set; }

    public string MissionId { get; set; } = null!;

    public string? CurrentStatus { get; set; }

    // 是否進入互動式同儕互評
    public bool IsPeerEvaluation { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Mission Mission { get; set; } = null!;
}
