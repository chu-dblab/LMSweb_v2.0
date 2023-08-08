namespace LMSweb.ViewModels.StudentManagement
{
    public class StudentCreateViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public Student? student { get; set; }
    }
}
