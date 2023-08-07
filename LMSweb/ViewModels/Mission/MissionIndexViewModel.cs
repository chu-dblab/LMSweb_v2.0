using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModels
{
    public class MissionIndexViewModel
    {
        public string CourseID { get; set; }
        [Display(Name = "課程名稱")]
        public string CourseName { get; set; }
        public int TestType { get; set; }
        public List<MissionData> Missions { get; set; }
    }

    public class MissionData
    {
        [Display(Name = "任務編號")]
        public string MID { get; set; }

        [Display(Name = "任務名稱")]
        public string Name { get; set; }

        [Display(Name = "任務開始時間")]
        public DateTime StartDate { get; set; }

        [Display(Name = "任務結束時間")]
        public DateTime EndDate { get; set; }

        [Display(Name = "任務內容")]
        public string? Detail { get; set; }
    }
}