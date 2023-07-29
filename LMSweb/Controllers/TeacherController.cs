using LMSweb.Data;
using LMSweb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private ILogger<TeacherController> _logger;
        private LMSContext _context;

        public TeacherController(ILogger<TeacherController> logger, LMSContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Home()
        {
            var tid = User.Claims.FirstOrDefault(x => x.Type == "UID").Value;   //抓出當初記載Claims陣列中的UID
            if (tid == null)
            {
                return Unauthorized();
            }
            var courses = _context.Courses.Where(c => c.TeacherId == tid).Select(x => new TeacherHomeViewModel
            {
                CourseID = x.Cid,
                CourseName = x.Cname,
                TestType = x.TestType
            });
            return View(courses.ToList());
        }
    }
}
