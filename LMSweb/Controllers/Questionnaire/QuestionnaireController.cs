using LMSweb.Data;
using LMSweb.Services;
using LMSweb.ViewModels.Questionnaire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers.Questionnaire
{
    [Authorize]
    public class QuestionnaireController : Controller
    {
        private readonly LMSContext _context;
        private readonly EprocedureSercices _eprocedureServices;
        private readonly EvaluationCoachingServices _evaluationCoachingServices;

        public QuestionnaireController(LMSContext context, EprocedureSercices eprocedureServices, 
            EvaluationCoachingServices evaluationCoachingServices)
        {
            _context = context;
            _eprocedureServices = eprocedureServices;
            _evaluationCoachingServices = evaluationCoachingServices;
        }

        public IActionResult Index(QuestionnaireIndexViewModel vm)
        {
            if(vm.CourseId == null || vm.MissionId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID");
            var eid = _eprocedureServices.GetEprocedureId(Convert.ToInt32(vm.TaskType), Convert.ToInt32(vm.TaskSteps));
            
            if (_eprocedureServices.IsAnswered(UID.Value, vm.MissionId, eid))
            {
                return RedirectToAction("Answer", "Questionnaire", new { cid = vm.CourseId, mid = vm.MissionId, EprocedureId = eid });
            }

            if(User.IsInRole("Student"))
            {
                vm.UID = UID.Value;
                vm.EvaluationGroupIdList = new List<string>();
                if (vm.EprocedureId == "6")
                {
                    var _EvaluationGroupIdList = _evaluationCoachingServices.GetEvaluationLeaderList(vm.MissionId, vm.UID);

                    foreach (var _EvaluationGroupId in _EvaluationGroupIdList)
                    {
                        var _EvaluationGroup = _context.EvaluationCoachings.Where(x => x.AUID == vm.UID && x.BUID == _EvaluationGroupId && x.MissionId == vm.MissionId).FirstOrDefault().Evaluation;
                        if (_EvaluationGroup == null && _EvaluationGroupId == vm.EvaluationGroupId)
                        {
                            vm.EvaluationGroupIdList.Add(_EvaluationGroupId);
                        }
                    }

                    if (vm.EvaluationGroupIdList.Count == 0)
                    {
                        return RedirectToAction("Details", "Mission", new { cid = vm.CourseId, mid = vm.MissionId });
                    }
                }
                else
                {
                    vm.EprocedureId = eid;
                }
            }

            return View(vm);
        }

        public IActionResult Answer(string cid, string mid, string EprocedureId)
        {
            var vm = new QuestionnaireAnswerViewModel();
            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID");

            vm.CourseId = cid;
            vm.MissionId = mid;
            vm.MissionName = _context.Missions.Find(mid).Mname;
            vm.Title = _context.ExperimentalProcedures.Find(EprocedureId).Name;

            vm.Answers = _eprocedureServices.GetAnswer(UID.Value, mid, EprocedureId);

            return View(vm);
        }
    }
}
