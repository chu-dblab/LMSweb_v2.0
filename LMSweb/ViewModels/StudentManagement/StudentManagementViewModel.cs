namespace LMSweb.ViewModels.StudentManagement
{
    public class StudentManagementViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }

        public List<Student> Students { get; set; }
    }

    public class Student
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentSex { get; set; }
        public string GroupName { get; set; }
    }
}
