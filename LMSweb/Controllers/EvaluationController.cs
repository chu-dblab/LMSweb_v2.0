using LMSweb.Data;
using LMSweb.Models;
using LMSweb.Services;
using LMSweb.ViewModels.Evaluation;
using LMSweb.ViewModels.Questionnaire;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace LMSweb.Controllers
{
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

                group.QuestionnaireIndexViewModel = new QuestionnaireIndexViewModel()
                {
                    /*
                     *  public string UID { get; set; }
                        public string CourseId { get; set; }
                        public string MissionId { get; set; }
                        public string MissionName { get; set; }
                        public string TaskType { get; set; }
                        public string TaskSteps { get; set; }
                        public string EprocedureId { get; set; }
                     */
                    UID = UID.Value,
                    CourseId = _context.Missions.Find(mid).CourseId,
                    MissionId = mid,
                    MissionName = _context.Missions.Find(mid).Mname,
                    TaskType = "4",
                    TaskSteps = "2",
                    EprocedureId = "6",
                    EvaluationGroupId = _EvaluationGroupId
                };

                int gid = _context.Students.Find(UID.Value).GroupId ?? 0;

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

        public IActionResult Show(string mid, string buid)
        {
            return View();
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
