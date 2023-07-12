using System;
using System.Collections.Generic;

namespace LMSweb.Models;

public partial class Group
{
    public int Gid { get; set; }

    public string Gname { get; set; } = null!;

    public virtual ICollection<Execution> Executions { get; set; } = new List<Execution>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
