namespace LMSweb.ViewModels.StudentManagement
{
    public class StudentManagementViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }

        public List<Student> Students { get; set; }
    }
}
