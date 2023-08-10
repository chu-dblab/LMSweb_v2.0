using LMSweb.Assets;
using LMSweb.Data;
using LMSweb.Services;
using LMSweb.ViewModels.Questionnaire;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers.Questionnaire
{
    public class QuestionnaireController : Controller
    {
        private readonly LMSContext _context;

        public QuestionnaireController(LMSContext context)
        {
            _context = context;
        }

        public IActionResult Index(QuestionnaireIndexViewModel vm)
        {
            var eprocedureServices = new EprocedureSercices(_context);

            if(vm.CourseId == null || vm.MissionId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID");
            var eid = eprocedureServices.GetEprocedureId(Convert.ToInt32(vm.TaskType), Convert.ToInt32(vm.TaskSteps));

            if (eprocedureServices.IsAnswered(UID.Value, vm.MissionId, eid))
            {
                return RedirectToAction("Answer", "Questionnaire", new { cid = vm.CourseId, mid = vm.MissionId, EprocedureId = eid });
            }

            vm.UID = UID.Value;

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

            var eprocedureServices = new EprocedureSercices(_context);

            vm.Answers = eprocedureServices.GetAnswer(UID.Value, mid, EprocedureId);

            return View(vm);
        }
    }
}
