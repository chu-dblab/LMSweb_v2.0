﻿using LMSweb.Assets;
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
                    GroupId = group.Gid.ToString(),
                    GroupName = group.Gname,
                    CurrentStatus = group.CurrentStatus,
                    StepsName = _StepsName,
                };

                var _EvaluationGroupIdList = new List<string>();
                var groupLeader = _context.Students.Where(x => x.GroupId == group.Gid && x.IsLeader == true).FirstOrDefault().StudentId;
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
                    _guideGroups.EvaluationName = new List<string>();
                    var EvaluationGroupLeaderList = _context.EvaluationCoachings.Where(x => x.AUID == groupLeader && x.MissionId == mid).Select(x => x.BUID).ToList();

                    foreach (var EvaGroupLeader in EvaluationGroupLeaderList)
                    {
                        var b_groupLeader = _context.Students.Where(x => x.StudentId == EvaGroupLeader).FirstOrDefault().GroupId;
                        var b_groupLeaderName = _context.Groups.Where(x => x.Gid == b_groupLeader).FirstOrDefault().Gname;

                        _guideGroups.EvaluationName.Add(b_groupLeaderName);
                    }

                    _guideGroups.EvaluationName.Sort();
                }

                if (TestType == 4 || TestType == 5)
                {
                    _guideGroups.CoachingName = new List<string>();
                    var CoachingGroupLeaderList = _context.EvaluationCoachings.Where(x => x.BUID == groupLeader && x.MissionId == mid).Select(x => x.AUID).ToList();

                    foreach (var CoachGroupLeader in CoachingGroupLeaderList)
                    {
                        var User = _context.Users.Where(x => x.Id == CoachGroupLeader).FirstOrDefault();

                        if (User.RoleName == "Student")
                        {
                            var a_groupLeader = _context.Students.Where(x => x.StudentId == CoachGroupLeader).FirstOrDefault().GroupId;
                            var a_groupLeaderName = _context.Groups.Where(x => x.Gid == a_groupLeader).FirstOrDefault().Gname;

                            _guideGroups.CoachingName.Add(a_groupLeaderName);
                        }
                    }
                    _guideGroups.CoachingName.Sort();
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
