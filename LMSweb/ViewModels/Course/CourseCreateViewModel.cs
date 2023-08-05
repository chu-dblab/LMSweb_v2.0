using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModels.Course
{
    public class CourseCreateViewModel
    {
        [Display(Name = "課程名稱")]
        [Required]
        public string Name { get; set; }
        [Required]
        public int TestType { get; set; }
    }
}
