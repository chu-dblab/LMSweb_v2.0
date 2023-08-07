using LMSweb.Assets;
using LMSweb.Data;
using LMSweb.Models;
using LMSweb.ViewModels;
using LMSweb.ViewModels.Mission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace LMSweb.Controllers
{
    [Authorize]
    public class MissionController : Controller
    {
        private readonly ILogger<MissionController> _logger;
        private readonly LMSContext _context;
        public MissionController(ILogger<MissionController> logger, LMSContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string cid)
        {
            if (cid == null)
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                if (role == null)
                {
                    return Unauthorized();
                }
                return RedirectToAction("Home", role.Value);
            }
            var mission_datas = _context.Missions
                .Where(c => c.CourseId == cid)
                .Select(m => new MissionData()
                {
                    MID = m.Mid,
                    Name = m.Mname,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate
                }).ToList();

            var course_data = _context.Courses.FirstOrDefault(c => c.Cid == cid);
            if (course_data == null)
            {
                return NotFound();
            }

            var mission_list = new MissionIndexViewModel()
            {
                CourseID = course_data.Cid,
                CourseName = course_data.Cname,
                TestType = course_data.TestType,
                Missions = mission_datas
            };

            return View(mission_list);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Create(string cid)
        {
            var course = _context.Courses.Find(cid);

            var createModel = new MissionCreateViewModel
            {
                CourseID = cid,
                CourseName = course.Cname ?? string.Empty,
            };
            return View(createModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(string cid, MissionCreateViewModel model)
        {
            //model.CourseID = cid;
            //model.CourseName = _context.Courses.Find(cid).Cname ?? string.Empty;
            PostData postData = model.PostData;
            if (postData != null)
            {
                var test_type = _context.Courses.Where(x => x.Cid == cid).Select(x => x.TestType).FirstOrDefault();
                var missionData = new Mission
                {
                    CourseId = cid,
                    Mid = $"M{DateTime.Now.ToString("yyMMddHHmmss")}",
                    Mname = postData.Name,
                    Detail = postData.Contents,
                    StartDate = DateTime.Parse(postData.StartDate),
                    EndDate = DateTime.Parse(postData.EndDate),
                    CurrentAction = GlobalClass.DefaultCurrentStatus(test_type)
                };
                _context.Missions.Add(missionData);
                _context.SaveChanges();

                //// 新增任務後，將該任務的資料新增至Executions資料表中
                //var students = _context.Students.Where(x => x.CourseId == cid && x.IsLeader == true).ToList();
                //foreach (var student in students)
                //{
                //    var executionData = new Execution
                //    {
                //        MissionId = missionData.Mid,
                //        GroupId = student.GroupId ?? 0,
                //        CurrentStatus = GlobalClass.DefaultCurrentStatus(test_type)
                //    };
                //    _context.Executions.Add(executionData);
                //    _context.SaveChanges();
                //}

                return RedirectToAction("Index", new { cid = cid });
            }
            return View(model);
        }

        public IActionResult Details(string cid, string mid)
        {
            var data = (from m in _context.Missions
                        from c in _context.Courses
                        where m.CourseId == c.Cid && m.Mid == mid && c.Cid == cid
                        select new MissionDetailViewModel
                        {
                            MissionID = m.Mid,
                            CourseID = c.Cid,
                            CourseName = c.Cname,
                            TestType = c.TestType,
                            Name = m.Mname,
                            Content = m.Detail,
                            StartDate = m.StartDate,
                            EndDate = m.EndDate,
                            CurrentAction = m.CurrentAction,
                        }).FirstOrDefault();
            if (data != null && data.CurrentAction is null)
            {
                data.CurrentAction = GlobalClass.DefaultCurrentStatus(data.TestType);
            }

            if (User.IsInRole("Student"))
            {
                var sid = User.Claims.FirstOrDefault(x => x.Type == "UID");   //抓出當初記載Claims陣列中的SID
                var student = _context.Students.Find(sid.Value);

                GuideForStudent _Guide = new GuideForStudent(mid, cid, sid.Value);
                _Guide.UpdateCurrentStatus();
                data.CurrentStatus = _context.Executions.Where(x => x.GroupId == student.GroupId && x.MissionId == mid).First().CurrentStatus;
                data.GroupName = _context.Groups.Where(x => x.Gid == student.GroupId).First().Gname;
                data.IsLeader = student.IsLeader;
            }

            return View(data);
        }
    }
}
