using LMSweb.Data;
using LMSweb.Models;
using LMSweb.Services;
using LMSweb.ViewModels;
using LMSweb.ViewModels.StudentManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

            var vm = _context.Courses.Select(x => new StudentManagementViewModel
            {
                CourseId = x.Cid,
                CourseName = x.Cname,
            })
                        .FirstOrDefault(x => x.CourseId == cid);
            if (vm == null)
            {
                return NotFound();
            }

            var student_related_data = (from s in _context.Students
                                        join g in _context.Groups on s.GroupId equals g.Gid into StudentGroup
                                        from g in StudentGroup.DefaultIfEmpty()
                                        where s.CourseId == cid
                                        select new ViewModels.StudentManagement.Student
                                        {
                                            StudentId = s.StudentId,
                                            StudentName = s.StudentName,
                                            StudentSex = s.Sex ?? "",
                                            GroupName = g.Gname,
                                        })
                                        .ToList();
            vm.Students = student_related_data;
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
        public IActionResult CreateStudent(string cid)
        {
            var vmodel = new StudentCreateViewModel();
            vmodel.CourseId = cid;
            var course = _context.Courses.Find(cid);

            if (course == null)
            {
                return NotFound();
            }

            vmodel.CourseName = course.Cname;

            return View(vmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStudent(StudentCreateViewModel vm)
        {
            if (vm != null)
            {
                var _student = new Models.Student()
                {
                    CourseId = vm.CourseId,
                    StudentId = vm.Student.StudentId,
                    StudentName = vm.Student.StudentName,
                    Sex = vm.Student.StudentSex,
                    IsLeader = false,
                };

                var _User = new User()
                {
                    Id = vm.Student.StudentId,
                    Upassword = HashHelper.SHA256Hash(vm.Student.StudentId),
                    Name = vm.Student.StudentName,
                    Gender = vm.Student.StudentSex,
                    RoleName = "Student"
                };

                _context.Users.Add(_User);
                _context.SaveChanges();
                _context.Students.Add(_student);
                _context.SaveChanges();
                return RedirectToAction("Index", "StudentManagement", new { cid = vm.CourseId });
            }
            return View(vm);
        }

        // GET: StudentEdit
        public IActionResult EditStudent(string sid, string cid)
        {
            if (sid == null)
            {
                return NotFound();
            }

            var student_data = (from s in _context.Students
                                join c in _context.Courses on s.CourseId equals c.Cid
                                where s.StudentId == sid && s.CourseId == cid
                                select new StudentCreateViewModel
                                {
                                    CourseId = c.Cid,
                                    CourseName = c.Cname,
                                    Student = new ViewModels.StudentManagement.Student
                                    {
                                        StudentId = s.StudentId,
                                        StudentName = s.StudentName,
                                        StudentSex = s.Sex
                                    }
                                })
                       .SingleOrDefault();



            if (student_data == null)
            {
                return NotFound();
            }

            return View(student_data);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditStudent(Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(student).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "StudentManagement", new { cid = student.CID });
        //    }

        //    return View(student);
        //}

        public IActionResult Group(string cid)
        {
            var course = _context.Courses.Find(cid);
            var student_list = _context.Students.Where(x => x.CourseId == cid);
            var student_has_group = student_list.Where(s => s.GroupId != null);
            var group_list = _context.Groups.Select(g => new ViewModels.StudentManagement.Group
            {
                GroupId = g.Gid,
                GroupName = g.Gname,
                Students = student_has_group.Where(s => s.GroupId == g.Gid)
                .Select(x => new ViewModels.StudentManagement.Student
                {
                    StudentId = x.StudentId,
                    StudentName = x.StudentName,
                    IsLeader = x.IsLeader
                }).OrderBy(x => x.StudentId).ToList()
            });

            var vm = new GroupViewModel
            {
                CourseId = course.Cid,
                CourseName = course.Cname,
                StudentList = new MultiSelectList(student_list.Where(x => x.GroupId == null), "StudentId", "StudentName").ToList(),
                Groups = group_list.OrderBy(x => x.GroupName.Length).ThenBy(x => x.GroupName).ToList(),
            };
            return View(vm);
        }

        public IActionResult ChangeLeader(int gid, string cid)
        {
            var groupStu = _context.Students.Where(s => s.GroupId == gid).ToList();
            var GName = _context.Groups.Find(gid).Gname;
            List<ChangeLeaderViewModel> changeLeaderViewModels = new List<ChangeLeaderViewModel>();

            if (groupStu == null)
            {
                return RedirectToAction("StudentGroup");
            }
            else
            {
                foreach (var stu in groupStu)
                {
                    changeLeaderViewModels.Add(new ChangeLeaderViewModel
                    {
                        GroupName = GName,
                        CourseID = cid,
                        StudentID = stu.StudentId,
                        StudentName = stu.StudentName,
                        IsLeader = stu.IsLeader
                    });
                }
            }
            return View(changeLeaderViewModels);
        }
        /*
        [HttpPost]
        public IActionResult ChangeLeader(string cid, string checkString)
        {
            var gid = db.Students.Find(chackString).GID;
            var groupStu = db.Students.Where(s => s.GID == gid).ToList();

            groupStu.ForEach(s => s.IsLeader = false);
            db.SaveChanges();
            groupStu.Find(s => s.SID == checkString).IsLeader = true;
            db.SaveChanges();

            return RedirectToAction("StudentGroup", new { cid = cid });
        }*/
    }
}
