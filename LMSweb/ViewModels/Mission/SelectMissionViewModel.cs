using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModels.Mission
{
    public class SelectMissionViewModel
    {

        public string CurrentCourseID { get; set; }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public int TestType { get; set; }
        public List<TaskData> Missions { get; set; }
    }
    public class TaskData
    {
        [Display(Name = "任務編號")]
        public string TaskID { get; set; }

        [Display(Name = "任務名稱")]
        public string Name { get; set; }

        [Display(Name = "任務內容")]
        public string TaskDetail { get; set; }

        [Display(Name = "任務開始時間")]
        public string StartDate { get; set; }

        [Display(Name = "任務結束時間")]
        public string EndDate { get; set; }
    }
}
