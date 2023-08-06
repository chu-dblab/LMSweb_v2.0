using LMSweb.Data;
using LMSweb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly LMSContext _context;

        public StudentController(ILogger<StudentController> logger, LMSContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Home()
        {
            var sid = User.Claims.FirstOrDefault(x => x.Type == "UID");   //抓出當初記載Claims陣列中的SID

            if (sid == null)
            {
                return Unauthorized();
            }

            var gid = (from s in _context.Students
                       where s.StudentId == sid.Value
                       select s.GroupId).FirstOrDefault();

            var results = (from c in _context.Courses
                           from s in _context.Students
                           from g in _context.Groups
                           from t in _context.Teachers
                           where s.CourseId == c.Cid && c.TeacherId == t.TeacherId && s.GroupId == g.Gid && s.GroupId == gid
                           select new
                           {
                               c.Cid,
                               c.Cname,
                               t.TeacherName,
                               g.Gname,
                               s.StudentName,
                               s.StudentId,
                               s.IsLeader
                           }).ToList();
            var data = results
                .Select(x => new StudentHomeViewModel
                {
                    CourseID = x.Cid,
                    CourseName = x.Cname,
                    TeacherName = x.TeacherName,
                    GroupName = x.Gname,
                    GroupStudent = new List<GroupStudentHomeViewModel>()
                }).FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            foreach (var item in results)
            {
                data.GroupStudent.Add(
                    new GroupStudentHomeViewModel
                    {
                        StudentID = item.StudentId,
                        StudentName = item.StudentName,
                        IsLeader = item.IsLeader
                    });
            }

            return View(data);
        }
    }
}
