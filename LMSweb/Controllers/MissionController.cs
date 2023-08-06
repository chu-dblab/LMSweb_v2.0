using LMSweb.Assets;
using LMSweb.Data;
using LMSweb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
