using LMSweb.Data;
using LMSweb.Services;
using LMSweb.ViewModels.Coaching;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    public class CoachingController : Controller
    {
        private readonly LMSContext _context;
        private readonly EprocedureSercices _eprocedureServices;
        private readonly EvaluationCoachingServices _evaluationCoachingServices;

        public CoachingController(LMSContext context, EprocedureSercices eprocedureServices, EvaluationCoachingServices evaluationCoachingServices)
        {
            _context = context;
            _eprocedureServices = eprocedureServices;
            _evaluationCoachingServices = evaluationCoachingServices;
        }

        public IActionResult Show(string mid, string auid, string? buid)
        {
            if(buid == null)
            {
                buid = User.Claims.FirstOrDefault(x => x.Type == "UID").Value;
            }

            var vm = new CoachingShowViewModel();
            vm.MissionId = mid;
            vm.CourseId = _context.Missions.Find(mid).CourseId;
            vm.MissionName = _context.Missions.Find(mid).Mname;

            var _CoachingGroup = _context.EvaluationCoachings.Where(x => x.AUID == auid && x.BUID == buid && x.MissionId == mid).FirstOrDefault().Coaching;
            if (_CoachingGroup != null)
            {
                var score_list = _CoachingGroup.Split(',').ToList();
                var CoaAnswer_list = new List<CoaAnswer>();

                Dictionary<string, string> scoreDict = new Dictionary<string, string>();

                scoreDict.Add("PC01", "");

                foreach (var score in score_list)
                {
                    var score_split = score.Split(':').ToList();

                    if (scoreDict.ContainsKey(score_split[0]))
                    {
                        scoreDict[key: score_split[0]] = _context.Options.Where(x => x.OptionID == int.Parse(score_split[1])).FirstOrDefault().Ocontent;
                    }
                    else
                    {
                        if (score_split.Count() > 1 && score_split[1] != "")
                        {
                            var question = _context.Questions.Find(score_split[0]).Qcontent;

                            CoaAnswer_list.Add(new CoaAnswer() { Question = question, Answer = score_split[1] });
                        }
                    }
                }

                vm.CoaPC = new CoaPC()
                {
                    PC01 = scoreDict["PC01"].ToString(),
                };

                vm.CoaAnswer = CoaAnswer_list;

            }

            return View(vm);
        }
    }
}
