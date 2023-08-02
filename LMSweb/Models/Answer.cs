using System;
using System.Collections.Generic;

namespace LMSweb.Models;

public partial class Answer
{
    public string Aid { get; set; } = null!;

    public string? Acontent { get; set; }

    public DateTime Atime { get; set; }

    public string QuestionId { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<Provided> Provideds { get; set; } = new List<Provided>();
}
