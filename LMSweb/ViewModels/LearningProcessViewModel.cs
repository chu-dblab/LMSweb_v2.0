namespace LMSweb.ViewModels
{
    public class LearningProcessViewModel
    {
        // 返回任務介面用
        public string CourseId { get; set; }
        public string MissionId { get; set; }


        // 這邊用於判斷要不要顯示切換組別模組
        public int TestType { get; set; }

        // 全班評價平均分數
        public ChartData CourseAgv { get; set; }
        // 小組評價平均分數
        public ChartData GroupAgv { get; set; }

        // 老師給所有小組的平均分數
        public ChartData TeacherAgv { get; set; }

        public List<DetailData> Detail { get; set; }
    }

    public class ChartData
    {
        //public string[] Labels { get; set; }
        public double[] Data { get; set; }
    }

    public class DetailData
    {
        // 評價
        public int[] Score { get; set; }
        public string[] TestFeedback { get; set; }

        // -------------------------------------------
        // 回饋
        public List<string>? ReTestFeedback { get; set; }
    }
}
