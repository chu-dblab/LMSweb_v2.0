using LMSweb.Data;
using LMSweb.Models;
using LMSweb.ViewModels.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMSweb.Controllers
{
    [Authorize]
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

            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID").Value;   //抓出當初記載Claims陣列中的TID

            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    TeacherId = UID,
                    CreateTime = DateTime.Now,
                    Cid = $"C{DateTime.Now.ToString("yyMMddHHmmss")}",
                    Cname = courseViewModel.Name,
                    TestType = courseViewModel.TestType,
                    // 以下欄位要根據TestType來決定其值，或是要刪除這兩個欄位
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

            var course = _context.Courses.FirstOrDefault(x => x.Cid == cid);
            var TeacherId = User.Claims.FirstOrDefault(x => x.Type == "UID").Value;
            var TeacherName = _context.Teachers.FirstOrDefault(x => x.TeacherId == TeacherId).TeacherName;
            if (course == null)
            {
                return NotFound();
            }
            var delsteViewModel = new CourseDeleteViewModel
            {
                TeacherName = TeacherName,
                CourseID = course.Cid,
                CourseName = course.Cname,
                TestType = course.TestType,
            };
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
