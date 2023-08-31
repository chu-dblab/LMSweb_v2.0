using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models;

public partial class Answer
{
    public string Aid { get; set; } = null!;

    public string? UserId { get; set; } = null!;
    public string? MissionId { get; set; } = null!;
    public string QuestionId { get; set; } = null!;

    public string? Acontent { get; set; }
    public DateTime Atime { get; set; }

    public virtual Question Question { get; set; } = null!;
    public virtual Mission Mission { get; set; } = null!;
    public virtual User User { get; set; } = null!;

    //public virtual ICollection<Provided> Provideds { get; set; } = new List<Provided>();
}
