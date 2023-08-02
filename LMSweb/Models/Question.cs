using System;
using System.Collections.Generic;

namespace LMSweb.Models;

public partial class Question
{
    public string QuestionId { get; set; } = null!;

    public string EprocedureId { get; set; } = null!;

    public string? CourseId { get; set; }

    public string? Qtype { get; set; }

    public string? Qcontent { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Course? Course { get; set; }

    public virtual ExperimentalProcedure Eprocedure { get; set; } = null!;

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();
}
