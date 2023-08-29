using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModels.StudentManagement
{
    public class StudentEditViewModel
    {
        public string? CourseId { get; set; }
        public string? CourseName { get; set; }

        [Display(Name = "姓名")]
        [Required]
        public string StudentName { get; set; } = "";

        [Display(Name = "性別")]
        [Required]
        public string StudentSex { get; set; } = "";
    }
}
