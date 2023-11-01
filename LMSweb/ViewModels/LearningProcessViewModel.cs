namespace LMSweb.ViewModels
{
    public class LearningProcessViewModel
    {
        // 返回任務介面用
        public string CourseId { get; set; }
        public string MissionId { get; set; }


        // 這邊用於判斷要不要顯示切換組別模組
        public int TestType { get; set; }

        public ChartData CourseAgv { get; set; }
        public ChartData GroupAgv { get; set; }

        public DetailData[] Detail { get; set; }
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
        public string[]? ReTestFeedback { get; set; }
    }
}
