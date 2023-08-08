namespace LMSweb.Models;

public partial class Course
{
    public string Cid { get; set; } = null!;

    public string? TeacherId { get; set; }

    public string Cname { get; set; } = null!;

    public required int TestType { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Teacher? Teacher { get; set; }
}
