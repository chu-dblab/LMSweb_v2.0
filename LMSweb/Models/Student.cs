using System;
using System.Collections.Generic;

namespace LMSweb.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public int? GroupId { get; set; }

    public string? CourseId { get; set; }

    public bool IsLeader { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Group? Group { get; set; }

    public virtual User StudentNavigation { get; set; } = null!;
}
