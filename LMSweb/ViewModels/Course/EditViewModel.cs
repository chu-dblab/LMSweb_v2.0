using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModels.Course
{
    public class EditViewModel
    {
        [Display(Name = "課程名稱")]
        public string CourseName { get; set; } = null!;
        public int TestType { get; set; } = default!;
    }
}
