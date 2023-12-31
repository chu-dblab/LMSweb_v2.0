﻿namespace LMSweb.Models;

public partial class Option
{
    public int OptionID { get; set; }

    public string Ocontent { get; set; } = null!;

    public string QuestionId { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
