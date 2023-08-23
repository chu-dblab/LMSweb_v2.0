using LMSweb.Assets;
using LMSweb.Data;
using LMSweb.Models;
using LMSweb.Services;
using LMSweb.ViewModels;
using LMSweb.ViewModels.Mission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

                // 新增任務後，將該任務的資料新增至Executions資料表中
                var students = _context.Students.Where(x => x.CourseId == cid && x.IsLeader == true).ToList();
                foreach (var student in students)
                {
                    var executionData = new Execution
                    {
                        MissionId = missionData.Mid,
                        GroupId = student.GroupId ?? 0,
                        CurrentStatus = GlobalClass.DefaultCurrentStatus(test_type)
                    };
                    _context.Executions.Add(executionData);
                    _context.SaveChanges();
                }

                var _EvaluationCoachingServices = new EvaluationCoachingServices(_context);
                _EvaluationCoachingServices.SetEvaluationCoaching(missionData.Mid);

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

                GuideForStudent _Guide = new GuideForStudent(mid, cid, _context, sid.Value);
                _Guide.UpdateCurrentStatus();
                data.CurrentStatus = _context.Executions.Where(x => x.GroupId == student.GroupId && x.MissionId == mid).First().CurrentStatus;
                data.GroupName = _context.Groups.Where(x => x.Gid == student.GroupId).First().Gname;
                data.IsLeader = student.IsLeader;
            }

            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(string mid, string cid)
        {
            var missionData = (from mission in _context.Missions
                               from course in _context.Courses
                               where mission.CourseId == course.Cid && mission.Mid == mid && mission.CourseId == cid
                               select new MissionCreateViewModel
                               {
                                   CourseID = course.Cid,
                                   CourseName = course.Cname,
                                   PostData = new PostData()
                                   {
                                       Name = mission.Mname,
                                       Contents = mission.Detail,
                                       StartDate = mission.StartDate.ToString("yyyy-MM-dd'T'HH:mm:ss.SSSz"),
                                       EndDate = mission.EndDate.ToString("yyyy-MM-dd'T'HH:mm:ss.SSSz"),
                                   }
                               })
                              .FirstOrDefault();
            return View(missionData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(string mid, string cid, MissionCreateViewModel model)
        {
            var postData = model.PostData;
            if (postData != null)
            {
                var original = _context.Missions
                    .Where(m => m.CourseId == cid && m.Mid == mid)
                    .FirstOrDefault();

                var test_type = _context.Courses.Where(x => x.Cid == cid).Select(x => x.TestType).FirstOrDefault();
                if (original.CurrentAction is null)
                {
                    original.CurrentAction = GlobalClass.DefaultCurrentStatus(test_type);
                }
                var newMission = new Mission
                {
                    CourseId = cid,
                    Mid = mid,
                    Mname = postData.Name,
                    Detail = postData.Contents,
                    StartDate = DateTime.Parse(postData.StartDate),
                    EndDate = DateTime.Parse(postData.EndDate),
                    CurrentAction = original.CurrentAction
                };
                _context.Entry(original).CurrentValues.SetValues(newMission);
                _context.SaveChanges();
                return RedirectToAction("Index", new { cid = newMission.CourseId });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(string mid, string cid)
        {
            if (mid == null || cid == null)
            {
                return NotFound();
            }

            var mission = _context.Missions
                .Where(m => m.CourseId == cid && m.Mid == mid)
                .FirstOrDefault();

            if (mission == null)
            {
                return View("Index", new { cid = cid });
            }

            var vm = new MissionIndexViewModel()
            {
                CourseID = cid,
                CourseName = _context.Courses.Find(cid).Cname ?? string.Empty,
                Missions = new List<MissionData>()
                {
                    new MissionData
                    {
                        MID = mission.Mid,
                        Name = mission.Mname,
                        StartDate = mission.StartDate,
                        EndDate = mission.EndDate,
                        Detail = mission.Detail,
                    }
                }
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(string mid, string cid, MissionIndexViewModel vm)
        {
            if(mid == null || cid == null)
            {
                return NotFound();
            }

            var mission = _context.Missions
                .Where(m => m.CourseId == cid && m.Mid == mid)
                .FirstOrDefault();

            if(mission == null)
            {
                return NotFound();
            } else
            {
                _context.Missions.Remove(mission);
                _context.SaveChanges();
                return RedirectToAction("Index", new { cid = cid });
            }
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult SelectCourses(string cid)
        {
            var tid = User.Claims.FirstOrDefault(x => x.Type == "UID").Value;
            var courses = _context.Courses.Where(c => c.TeacherId == tid).Select(x => new TeacherHomeViewModel
            {
                CourseID = x.Cid,
                CourseName = x.Cname,
                TestType = x.TestType
            });
            ViewData["CurrentCourseID"] = cid;
            return View(courses.ToList());
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult SelectMissions(string selectedCID, string currentCID)
        {
            var tasklist = _context.Missions.Where(m => m.CourseId == selectedCID).Select(s => new TaskData
            {
                TaskID = s.Mid,
                Name = s.Mname,
                TaskDetail = s.Detail,
                StartDate = s.StartDate.ToString(),
                EndDate = s.StartDate.ToString()
            }).ToList();

            var course = _context.Courses.Find(selectedCID);
            var data = new SelectMissionViewModel
            {
                CurrentCourseID = currentCID,
                CourseID = course.Cid,
                CourseName = course.Cname,
                TestType = course.TestType,
                Missions = tasklist
            };
            return View(data);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Copy(string mid, string selectedCID, string currentCID)
        {
            var missionData = (from mission in _context.Missions
                               from course in _context.Courses
                               where mission.CourseId == course.Cid && mission.Mid == mid && course.Cid == selectedCID
                               select new MissionCreateViewModel
                               {
                                   CourseID = course.Cid,
                                   CourseName = course.Cname,
                                   PostData = new PostData()
                                   {
                                       MID = mission.Mid,
                                       Name = mission.Mname,
                                       Contents = mission.Detail,
                                       StartDate = mission.StartDate.ToString("yyyy-MM-dd'T'HH:mm:ss.SSSz"),
                                       EndDate = mission.EndDate.ToString("yyyy-MM-dd'T'HH:mm:ss.SSSz"),
                                   }
                               }).FirstOrDefault();
            ViewData["CurrentCourseID"] = currentCID;
            return View(missionData);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Copy(string currentCID, MissionCreateViewModel formdata)
        {
            if (formdata != null)
            {
                var test_type = _context.Courses.Where(x => x.Cid == currentCID).Select(x => x.TestType).FirstOrDefault();
                var missionData = new Mission
                {
                    CourseId = currentCID,
                    Mid = $"M{DateTime.Now.ToString("yyMMddHHmmss")}",
                    Mname = formdata.PostData.Name,
                    Detail = formdata.PostData.Contents,
                    StartDate = DateTime.Parse(formdata.PostData.StartDate),
                    EndDate = DateTime.Parse(formdata.PostData.EndDate),
                    CurrentAction = GlobalClass.DefaultCurrentStatus(test_type)
                };
                formdata.CourseID = currentCID;
                _context.Missions.Add(missionData);
                _context.SaveChanges();

                // 新增任務後，將該任務的資料新增至Executions資料表中
                var students = _context.Students.Where(x => x.CourseId == currentCID && x.IsLeader == true).ToList();
                foreach (var student in students)
                {
                    var executionData = new Execution
                    {
                        MissionId = missionData.Mid,
                        GroupId = student.GroupId ?? 0,
                        CurrentStatus = GlobalClass.DefaultCurrentStatus(test_type)
                    };
                    _context.Executions.Add(executionData);
                    _context.SaveChanges();
                }

                var _EvaluationCoachingServices = new EvaluationCoachingServices(_context);
                _EvaluationCoachingServices.SetEvaluationCoaching(missionData.Mid);

                return RedirectToAction("Index", new { cid = formdata.CourseID });
            }

            return View(formdata);
        }
    }
}
