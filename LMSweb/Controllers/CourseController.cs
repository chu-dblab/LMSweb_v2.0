using LMSweb.Data;
using LMSweb.Models;
using LMSweb.ViewModels.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class CourseController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly LMSContext _context;

        public CourseController(ILogger<StudentController> logger, LMSContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseCreateViewModel courseViewModel)
        {
            /*
             * 新增課程到資料庫
             */

            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID");   //抓出當初記載Claims陣列中的TID
            if (UID == null)
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    TeacherId = UID.Value,
                    CreateTime = DateTime.Now,
                    Cid = $"C{DateTime.Now.ToString("yyMMddHHmmss")}",
                    Cname = courseViewModel.Name,
                    TestType = courseViewModel.TestType,
                };
                _context.Courses.Add(course);
                _context.SaveChanges();
                //return RedirectToAction("Home", "Teacher");
                return RedirectToAction("Index", "StudentManagement", new { cid = course.Cid, firstCreate = true });
            }
            return View(courseViewModel);
        }

        public ActionResult Edit(string cid)
        {
            /*
             * 透過id找到課程，並將資料傳到 View                   
             */

            var course = _context.Courses.FirstOrDefault(x => x.Cid == cid);
            if (course == null)
            {
                return NotFound();
            }
            var courseViewModel = new CourseEditViewModel
            {
                CourseName = course.Cname,
                TestType = course.TestType,
            };
            return View(courseViewModel);
        }

        [HttpPost]
        public ActionResult Edit(string cid, CourseEditViewModel courseViewModel)
        {
            /*
             *  透過id找到課程，並將資料更新到資料庫
             */

            var course = _context.Courses.FirstOrDefault(x => x.Cid == cid);
            if (course == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                course.Cname = courseViewModel.CourseName;
                course.TestType = courseViewModel.TestType;

                _context.SaveChanges();
                return RedirectToAction("Home", "Teacher");
            }
            return View(courseViewModel);
        }

        public ActionResult Delete(string cid)
        {
            /*
             * 透過id找到課程，並將資料傳到 View                   
             */

            var TeacherId = User.Claims.FirstOrDefault(x => x.Type == "UID");
            if (TeacherId == null)
            {
                return Unauthorized();
            }

           var delsteViewModel = (from c in _context.Courses
                       join t in _context.Teachers on c.TeacherId equals TeacherId.Value
                       where c.Cid == cid
                       select new CourseDeleteViewModel
                       {
                           TeacherName = t.TeacherName,
                           CourseID = c.Cid,
                           CourseName = c.Cname,
                           TestType = c.TestType,
                       })
                       .FirstOrDefault();
            return View(delsteViewModel);
        }

        [HttpPost]
        public ActionResult Delete(string cid, CourseDeleteViewModel courseViewModel)
        {
            /*
             * 透過id找到課程，並將資料從資料庫刪除
             */

            var course = _context.Courses.FirstOrDefault(x => x.Cid == cid);
            if (course == null)
            {
                return NotFound();
            } else
            {
                // 刪除 Executions 表中的資料
                var missions_list = _context.Missions.Where(x => x.CourseId == cid).ToList();
                foreach (var mission in missions_list)
                {
                    var executions = _context.Executions.Where(x => x.MissionId == mission.Mid);
                    foreach (var execution in executions)
                    {
                        _context.Executions.Remove(execution);
                        _context.SaveChanges();
                    }
                }

                // 依據 Course 刪除 mission 表中的資料
                var missions = _context.Missions.Where(x => x.CourseId == cid);
                foreach (var mission in missions)
                {
                    _context.Missions.Remove(mission);
                }
                _context.SaveChanges();

                // 依據 CourseStudents_list 刪除 student 表中的資料
                var CourseStudents_list = _context.Students.Where(x => x.CourseId == cid).ToList();
                var CourseStudents = _context.Students.Where(x => x.CourseId == cid);
                foreach (var student in CourseStudents)
                {
                    _context.Students.Remove(student);
                }
                _context.SaveChanges();

                // 依據 CourseStudents_list 刪除 user 表中的資料
                foreach (var student in CourseStudents_list)
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == student.StudentId);
                    if (user != null)
                        _context.Users.Remove(user);
                }
                _context.SaveChanges();
                
                // 刪除組別 group
                var groups = _context.Groups.Where(x => x.CourseId == cid);
                foreach (var group in groups)
                {
                    _context.Groups.Remove(group);
                }
                _context.SaveChanges();

                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            return RedirectToAction("Home", "Teacher");
        }
    }
}
