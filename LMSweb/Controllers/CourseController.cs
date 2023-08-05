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
        public ActionResult Create(CreateViewModel courseViewModel)
        {
            /*
             *  public string Cid { get; set; } = null!;

                public string? TeacherId { get; set; }

                public string Cname { get; set; } = null!;

                public required int TestType { get; set; }

                public DateTime CreateTime { get; set; }
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
    }
}
