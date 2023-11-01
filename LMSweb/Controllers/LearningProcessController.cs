using LMSweb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    public class LearningProcessController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new LearningProcessViewModel
            {
                CourseId = "C0001",
                MissionId = "M0001",
                TestType = 1,
                CourseAgv = new ChartData
                {
                    Data = new double[] { 1, 2, 3 }
                },
                GroupAgv = new ChartData
                {
                    Data = new double[] { 2, 1, 2 }
                },
                Detail = new DetailData[]
                {
                    new DetailData
                    {
                        Score = new int[] { 1, 2, 3 },
                        TestFeedback = new string[] { "你們小組程式碼寫得很好", "你們小組流程圖畫得不錯", "加油" },
                    },
                    new DetailData
                    {
                        Score = new int[] { 2, 2, 3 },
                        TestFeedback = new string[] { "1", "2", "3" },
                        ReTestFeedback = new string[] { "非常合理", "你們小組流程圖畫得不錯" }
                    },
                    new DetailData
                    {
                        Score = new int[] { 3, 2, 3 },
                        TestFeedback = new string[] { "1", "2", "3" },
                    },
                    new DetailData
                    {
                        Score = new int[] { 1, 1, 3 },
                        TestFeedback = new string[] { "1", "2", "3" },
                        ReTestFeedback = new string[] { "1", "2", "3" }
                    }
                }
            };

            return View(viewModel);
        }
    }
}
