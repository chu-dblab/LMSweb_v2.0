using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "學號")]
        public string StudentId { get; set; }
        [Display(Name = "姓名")]
        public string StudentName { get; set; }
        [Display(Name = "性別")]
        public string StudentSex { get; set; }
        [Display(Name = "組別")]
        
        public string? GroupName { get; set; }
        public bool? IsStudent { get; set; }
    }
}
