using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSweb.ViewModels.StudentManagement
{
    public class GroupViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public List<Group>? Groups { get; set; }
    }
}
