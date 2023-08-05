using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModels.Course
{
    public class CourseEditViewModel
    {
        [Display(Name = "課程名稱")]
        public string CourseName { get; set; } = null!;
        public int TestType { get; set; } = default!;
    }
}
