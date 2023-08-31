namespace LMSweb.ViewModels.Mission
{
    public class TaskGuideStapPartialViewModel
    {
        public string StapName { get; set; }
        public string CurrentAction { get; set; }
        public int k { get; set; }
        public string buttonLink { get; set; }

        // 判斷是否需要給 URL 連結
        public int TestType { get; set; }
        public bool IsLeader { get; set; }
    }
}
