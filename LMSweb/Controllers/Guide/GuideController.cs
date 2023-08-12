using LMSweb.Assets;
using LMSweb.Data;
using LMSweb.ViewModels.Guide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers.Guide
{
    [Authorize(Roles = "Teacher")]
    public class GuideController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly LMSContext _context;

        public GuideController(ILogger<StudentController> logger, LMSContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public IActionResult Management(string mid)
        {
            var uid = User.Claims.FirstOrDefault(x => x.Type == "UID");

            var cid = _context.Missions.Find(mid).CourseId;

            var vm = new GuideManagementViewModel
            {
                CourseId = cid,
                MissionId = mid,
                MissionName = _context.Missions.Find(mid).Mname,
            };

            var groups = (  from g in _context.Groups
                            from e in _context.Executions
                            where g.Gid == e.GroupId && e.MissionId == mid
                            select new
                            {
                                g.Gid,
                                g.Gname,
                                e.CurrentStatus,
                            }).ToList();

            var TestType = _context.Courses.Where(x => x.Cid == cid).FirstOrDefault().TestType;

            List<GuideGroup> guideGroups = new List<GuideGroup>();

            var TaskStepsList = GlobalClass.GetTaskStepsString(TestType);
            var _StepsName = new List<string>();
            
            foreach (var taskStep in TaskStepsList)
            {
                var sn = _context.ExperimentalProcedures.Where(x => x.EprocedureId == taskStep).FirstOrDefault().Name;
                _StepsName.Add(sn);
            }
            _StepsName.Add("任務完成");

            foreach (var group in groups)
            {
                guideGroups.Add(new GuideGroup
                {
                    GroupId = group.Gid.ToString(),
                    GroupName = group.Gname,
                    CurrentStatus = group.CurrentStatus,
                    StepsName = _StepsName,
                });
            }

            vm.Groups = guideGroups;

            return View(vm);
        }

        public IActionResult _GuideGroupPartial()
        {
            return PartialView();
        }
    }
}
