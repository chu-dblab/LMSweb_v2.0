using System;
using System.Collections.Generic;

namespace LMSweb.Models;

public partial class Teacher
{
    public string TeacherId { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual User TeacherNavigation { get; set; } = null!;
}
