using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSweb.ViewModels.StudentManagement
{
    public class GroupViewModel
    {
        public string CourseId { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public List<group>? groups { get; set; }
    }

    public class group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public List<Student> students { get; set; }
    }
}
