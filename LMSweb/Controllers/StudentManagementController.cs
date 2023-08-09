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
                Students = _context.Students.Where(x => x.CourseId == cid).Select(x => new ViewModels.StudentManagement.Student
                {
                    StudentId = x.StudentId,
                    StudentName = x.StudentName,
                    StudentSex = x.Sex,
                    GroupName = _context.Groups.FirstOrDefault(y => y.Gid == x.GroupId).Gname
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string cid, IFormFile file)
        {
           var students = await _fileUploadService.Upload(file);
            if (students == null)
            {
                return BadRequest();
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

        // GET: StudentCreate
        public ActionResult StudentCreate(string cid)
        {
            var vmodel = new StudentCreateViewModel();
            vmodel.CourseId = cid;
            var course = _context.Courses.Where(c => c.Cid == cid).Single();

            vmodel.CourseName = course.Cname;

            return View(vmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentCreate(StudentCreateViewModel vm)
        {
            if (vm != null)
            {
                var _student = new Models.Student()
                {
                    CourseId = vm.CourseId,
                    StudentId = vm.student.StudentId,
                    StudentName = vm.student.StudentName,
                    Sex = vm.student.StudentSex,
                    IsLeader = false,
                };

                var _User = new User()
                {
                    Id = vm.student.StudentId,
                    Upassword = HashHelper.SHA256Hash(vm.student.StudentId),
                    Name = vm.student.StudentName,
                    Gender = vm.student.StudentSex,
                    RoleName = "Student"
                };

                _context.Users.Add(_User);
                _context.SaveChanges();
                _context.Students.Add(_student);
                _context.SaveChanges();
                return RedirectToAction("Index","StudentManagement", new { cid = vm.CourseId });
            }
            return View(vm);
        }

        // GET: StudentEdit
        public ActionResult StudentEdit(string sid, string cid)
        {
            if (sid == null)
            {
                return NotFound();
            }

            var student = _context.Students.Where(s => s.StudentId == sid).Single();
            var vm = new StudentCreateViewModel()
            {
                CourseId = cid,
                student = new ViewModels.StudentManagement.Student
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    StudentSex = student.Sex,
                },
            };


            if (student == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult StudentEdit([Bind(Include = "SID,CourseID,SName,SPassword,Sex,Score")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(student).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("StudentManagement", "Course", new { student.CID });
        //    }

        //    return View(student);
        //}

        public IActionResult Group()
        {
            var vm = new GroupViewModel();

            //var sid = User.Claims.FirstOrDefault(x => x.Type == "UID");

            //vm.CourseId = _context.Students.Where(x => x.StudentId == sid.Value).FirstOrDefault().CourseId;
            

            return View(vm);
        }
    }
}
