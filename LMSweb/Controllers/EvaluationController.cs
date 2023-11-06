using LMSweb.Data;
using LMSweb.Services;
using LMSweb.ViewModels.Evaluation;
using LMSweb.ViewModels.Questionnaire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Authorize]
    public class EvaluationController : Controller
    {
        private readonly LMSContext _context;
        private readonly EprocedureSercices _eprocedureServices;
        private readonly EvaluationCoachingServices _evaluationCoachingServices;

        public EvaluationController(LMSContext context, EprocedureSercices eprocedureServices, EvaluationCoachingServices evaluationCoachingServices)
        {
            _context = context;
            _eprocedureServices = eprocedureServices;
            _evaluationCoachingServices = evaluationCoachingServices;
        }

        public IActionResult Management(string mid)
        {
            var vm = new EvaluationManagementViewModel();
            vm.EvaGroupList = new List<EvaGroup>();
            vm.MissionId = mid;
            vm.CourseId = _context.Missions.Find(mid).CourseId;

            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID");

            var _EvaluationGroupIdList = _evaluationCoachingServices.GetEvaluationLeaderList(mid, UID.Value);

            foreach (var _EvaluationGroupId in _EvaluationGroupIdList)
            {
                var group = new EvaGroup();
                group.GroupLeaderId = _EvaluationGroupId;
                group.IsSubmitted = false;
                group.IsAnswered = false;

                // 這邊因為是後來加上去 測試步驟都先強制使用這個值
                group.QuestionnaireIndexViewModel = new QuestionnaireIndexViewModel()
                {
                    UID = UID.Value,
                    CourseId = _context.Missions.Find(mid).CourseId,
                    MissionId = mid,
                    MissionName = _context.Missions.Find(mid).Mname,
                    TaskType = "4",
                    TaskSteps = "2",
                    EprocedureId = "6",
                    EvaluationGroupId = _EvaluationGroupId
                };

                int gid = _context.Students.Find(_EvaluationGroupId).GroupId ?? 0;

                if (gid == 0)
                {
                    return RedirectToAction("Details", "Mission", new { mid = mid });
                } else
                {
                    group.IsSubmitted = HasDraw(gid, mid);
                }
                

                var _EvaluationGroup = _context.EvaluationCoachings.Where(x => x.AUID == UID.Value && x.BUID == _EvaluationGroupId && x.MissionId == mid).FirstOrDefault().Evaluation;

                if (_EvaluationGroup != null)
                {
                    group.IsAnswered = true;
                }

                vm.EvaGroupList.Add(group);
            }

            if (_EvaluationGroupIdList.Count == 0)
            {
                return RedirectToAction("Details", "Mission", new { mid = mid , cid = vm.CourseId });
            }

            return View(vm);
        }

        public IActionResult Show(string mid, string buid, string? auid)
        {
            if(auid == null)
            {
                auid = User.Claims.FirstOrDefault(x => x.Type == "UID").Value;
            }

            var vm = new EvaluationShowViewModel();
            vm.MissionId = mid;
            vm.CourseId = _context.Missions.Find(mid).CourseId;
            vm.MissionName = _context.Missions.Find(mid).Mname;

            var _EvaluationGroup = _context.EvaluationCoachings.Where(x => x.AUID == auid && x.BUID == buid && x.MissionId == mid).FirstOrDefault().Evaluation;
            if (_EvaluationGroup != null)
            {
                var score_list = _EvaluationGroup.Split(',').ToList();
                var EvaAnswer_list = new List<EvaAnswer>();

                Dictionary<string, int> scoreDict = new Dictionary<string, int>();

                scoreDict.Add("PE01", 0);
                scoreDict.Add("PE02", 0);
                scoreDict.Add("PE03", 0);

                foreach (var item in score_list)
                {
                    var score = item.Split(':').ToList();
                    if (scoreDict.ContainsKey(score[0]))
                    {
                        scoreDict[key: score[0]] = _evaluationCoachingServices.GetScore(int.Parse(score[1]));
                    }
                    else
                    {
                        if (score.Count() > 1 && score[1] != "")
                        {
                            var question = _context.Questions.Find(score[0]).Qcontent;

                            EvaAnswer_list.Add(new EvaAnswer() { Question = question, Answer = score[1] });
                        }
                    }
                }

                vm.EvaPE = new EvaPE()
                {
                    PE01 = scoreDict["PE01"].ToString(),
                    PE02 = scoreDict["PE02"].ToString(),
                    PE03 = scoreDict["PE03"].ToString()
                };

                vm.EvaAnswer = EvaAnswer_list;
            }

            return View(vm);
        }

        private bool HasDraw(int gid, string mid)
        {
            var Draw = _context.ExecutionContents.Where(x => x.GroupId == gid && x.MissionId == mid && x.Type == "D").FirstOrDefault();
            if (Draw != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
