using LMSweb.Assets;
using LMSweb.Data;
using LMSweb.ViewModels.Guide;
using LMSweb.ViewModels.Questionnaire;
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
                var _guideGroups = new GuideGroup
                {
                    MissionId = mid,
                    GroupId = group.Gid.ToString(),
                    GroupName = group.Gname,
                    CurrentStatus = group.CurrentStatus,
                    StepsName = _StepsName,
                };

                var _EvaluationGroupIdList = new List<string>();
                var groupLeader = _context.Students.Where(x => x.GroupId == group.Gid && x.IsLeader == true).FirstOrDefault().StudentId;
                _guideGroups.GroupLeaderId = groupLeader;
                _EvaluationGroupIdList.Add(groupLeader);

                var _questionnaireIndexViewModel = new QuestionnaireIndexViewModel
                {
                    UID = uid.Value,
                    CourseId = cid,
                    MissionId = mid,
                    MissionName = _context.Missions.Find(mid).Mname,
                    TaskType = "5",
                    TaskSteps = "4",
                    EprocedureId = "6",
                    EvaluationGroupIdList = _EvaluationGroupIdList
                };

                _guideGroups.questionnaireIndexViewModel = _questionnaireIndexViewModel;

                if (TestType == 2 || TestType == 4 || TestType == 5)
                {
                    _guideGroups.Evaluation = new List<EvalustionGroup>();
                    var EvaluationGroupLeaderList = _context.EvaluationCoachings.Where(x => x.AUID == groupLeader && x.MissionId == mid).Select(x => x.BUID).ToList();

                    foreach (var EvaGroupLeader in EvaluationGroupLeaderList)
                    {
                        var b_groupId = _context.Students.Where(x => x.StudentId == EvaGroupLeader).FirstOrDefault().GroupId;
                        var b_groupLeaderName = _context.Groups.Where(x => x.Gid == b_groupId).FirstOrDefault().Gname;

                        var EvalustionGroup = new EvalustionGroup
                        {
                            EvaluationName = b_groupLeaderName,
                            EvalustionLeaderId = null,
                        };
                        
                        var _EvaluationGroup = _context.EvaluationCoachings.Where(x => x.AUID == groupLeader && x.BUID == EvaGroupLeader && x.MissionId == mid).FirstOrDefault().Evaluation;

                        if (_EvaluationGroup != null)
                        {
                            EvalustionGroup.EvalustionLeaderId = EvaGroupLeader;
                            EvalustionGroup.IsSubmit = true;
                        } 
                        else
                        {
                            var Draw = _context.ExecutionContents.Where(x => x.GroupId == b_groupId && x.MissionId == mid && x.Type == "D").FirstOrDefault();
                            if (Draw != null)
                            {
                                EvalustionGroup.IsSubmit = true;
                            }
                        }

                        _guideGroups.Evaluation.Add(EvalustionGroup);
                    }

                }

                if (TestType == 4 || TestType == 5)
                {
                    _guideGroups.Coaching = new List<ViewModels.Guide.CoachingGroup>();

                    var CoachingGroupLeaderList = _context.EvaluationCoachings.Where(x => x.BUID == groupLeader && x.MissionId == mid).Select(x => x.AUID).ToList();

                    foreach (var CoachGroupLeader in CoachingGroupLeaderList)
                    {
                        var User = _context.Users.Where(x => x.Id == CoachGroupLeader).FirstOrDefault();

                        if (User.RoleName == "Student")
                        {
                            var a_groupId = _context.Students.Where(x => x.StudentId == CoachGroupLeader).FirstOrDefault().GroupId;
                            var a_groupLeaderName = _context.Groups.Where(x => x.Gid == a_groupId).FirstOrDefault().Gname;
                            
                            var CoachingGroup = new ViewModels.Guide.CoachingGroup
                            {
                                CoachingName = a_groupLeaderName,
                                CoachingLeaderId = null,
                            };
                            
                            if(_context.EvaluationCoachings.Where(x => x.AUID == CoachGroupLeader && x.BUID == groupLeader && x.MissionId == mid).FirstOrDefault().Coaching != null)
                            {
                                CoachingGroup.CoachingLeaderId = CoachGroupLeader;
                                CoachingGroup.IsSubmit = true;
                            }
                            else
                            {
                                if (_context.EvaluationCoachings.Where(x => x.AUID == CoachGroupLeader && x.BUID == groupLeader && x.MissionId == mid).FirstOrDefault().Evaluation != null)
                                {
                                    CoachingGroup.IsSubmit = true;
                                }
                            }

                            _guideGroups.Coaching.Add(CoachingGroup);
                        }
                    }
                }

                // 這邊會有問題
                var _TeacherEva = _context.EvaluationCoachings.Where(x => x.AUID == uid.Value && x.BUID == groupLeader && x.MissionId == mid).FirstOrDefault();

                if(_TeacherEva.Evaluation != null)
                {
                    _guideGroups.IsCorrect = true;
                }

                guideGroups.Add(_guideGroups);
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
