namespace LMSweb.Models;

public partial class Teacher
{
    public string TeacherId { get; set; } = null!;

    public string TeacherName { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
     
    public virtual User User { get; set; } = null!;
}
