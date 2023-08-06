using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class MissionDetailViewModel
    {
        public string CourseID { get; set; }
        public string MissionID { get; set; }
        public string CourseName { get; set; }
        public int TestType { get; set; }
        [Display(Name = "任務名稱")]
        public string Name { get; set; }
        [Display(Name = "任務內容")]
        public string Content { get; set; }
        [Display(Name = "開始時間")]
        public DateTime StartDate { get; set; }
        [Display(Name = "結束時間")]
        public DateTime EndDate { get; set; }
        public string CurrentAction { get; set; }
        public string CurrentStatus { get; set; }
        public string GroupName { get; set; }
        public bool IsLeader { get; set; }
    }
}