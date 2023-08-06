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
                return RedirectToAction("Home", "Teacher");
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
            }
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Home", "Teacher");
        }
    }
}
