using LMSweb.Data;
using LMSweb.ViewModels.StudentManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace LMSweb.Controllers
{
    [Authorize]
    public class StudentManagementController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly LMSContext _context;

        public StudentManagementController(ILogger<StudentController> logger, LMSContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string cid)
        {
            if (cid == null)
            {
                return NotFound();
            }

            var vm = new StudentManagementViewModel()
            {
                CourseId = cid,
                CourseName = _context.Courses.FirstOrDefault(x => x.Cid == cid).Cname,
                Students = _context.Students.Where(x => x.CourseId == cid).Select(x => new Student
                {
                    StudentId = x.StudentId,
                    StudentName = x.StudentName,
                    StudentSex = x.Sex,
                    GroupName = _context.Groups.FirstOrDefault(y => y.Gid == x.GroupId).Gname
                }).ToList()
            };

            return View(vm);
        }
    }
}
