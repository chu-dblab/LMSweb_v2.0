namespace LMSweb.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Upassword { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Gender { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual Student? Student { get; set; }

    public virtual Teacher? Teacher { get; set; }

    public virtual Provided? Provided { get; set; }

    public virtual ICollection<EvaluationCoaching>? EvaluationCoachingAUsers { get; set; }
    public virtual ICollection<EvaluationCoaching>? EvaluationCoachingBUsers { get; set; }

}
