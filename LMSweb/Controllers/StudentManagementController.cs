using LMSweb.Data;
using LMSweb.Models;
using LMSweb.Services;
using LMSweb.ViewModels.StudentManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class StudentManagementController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly LMSContext _context;
        private readonly FileUploadService _fileUploadService;

        public StudentManagementController(ILogger<StudentController> logger, LMSContext context, FileUploadService fileUpload)
        {
            _logger = logger;
            _context = context;
            _fileUploadService = fileUpload;
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

        public async Task<IActionResult> Upload(string cid, IFormFile uploaded_file)
        {
           var students = await _fileUploadService.Upload(uploaded_file);
            if (students == null)
            {
                return RedirectToAction("Index", new { cid = cid });
            }
            foreach (var item in students)
            {
                _context.Users.Add(new User
                {
                    Id = item.StudentId,
                    Name = item.StudentName,
                    Upassword = HashHelper.SHA256Hash(item.StudentId),
                    RoleName = "Student"
                });
                _context.Students.Add(new Models.Student
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    CourseId = cid,
                    Sex = item.StudentSex
                });
                _context.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}
