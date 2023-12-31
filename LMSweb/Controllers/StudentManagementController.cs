﻿using LMSweb.Data;
using LMSweb.Models;
using LMSweb.Services;
using LMSweb.ViewModels.StudentManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Index(string cid, bool? firstCreate)
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

            vm.FirstCreate = firstCreate ?? false;

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

            // 檢查學生資料是否有重複
            var studentIds = students.Select(x => x.StudentId).ToList();
            foreach (var item in studentIds)
            {
                if (studentIds.Where(x => x == item).Count() > 1)
                {
                    return BadRequest("學生資料有重複");
                }
            }

            // 檢查學號是否已經存在SQL
            var studentIdsInSQL = _context.Students.Select(x => x.StudentId).ToList();
            foreach (var item in studentIds)
            {
                if (studentIdsInSQL.Contains(item))
                {
                    return BadRequest("學號已經存在");
                }
            }

            // 檢查學生性別是否為男或女
            foreach (var item in students)
            {
                if (item.StudentSex != "男" && item.StudentSex != "女")
                {
                    return BadRequest("學生性別必須為男或女");
                }
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
                                select new StudentEditViewModel
                                {
                                    CourseId = c.Cid,
                                    CourseName = c.Cname,
                                    StudentName = s.StudentName,
                                    StudentSex = s.Sex ?? "",
                                })
                       .SingleOrDefault();



            if (student_data == null)
            {
                return NotFound();
            }

            return View(student_data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(string sid, string cid, StudentEditViewModel student_data)
        {
            if (ModelState.IsValid)
            {
                var student = _context.Students.SingleOrDefault(x => x.StudentId == sid && x.CourseId == cid);
                if (student == null)
                {
                    return RedirectToAction("Index", "StudentManagement", new { cid = student_data.CourseId });
                }
                student.StudentName = student_data.StudentName;
                student.Sex = student_data.StudentSex;
                _context.SaveChanges();
                return RedirectToAction("Index", "StudentManagement", new { cid = cid });
            }
            return View(student_data);
        }

        public IActionResult Group(string cid)
        {
            if (cid == null)
            {
                return RedirectToAction("Home", "Teacher");
            }
            var course = _context.Courses.Find(cid);
            if (course == null)
            {
                return RedirectToAction("Index", "StudentManagement", new { cid = cid });
            }

            var student_list = _context.Students.Where(x => x.CourseId == cid && x.GroupId == null).ToList();


            var data = (from s in _context.Students
                        join g in _context.Groups on s.GroupId equals g.Gid
                        where s.CourseId == cid && g.CourseId == cid
                        select new { g.Gid, g.Gname, s.StudentId, s.StudentName, s.Sex, s.IsLeader })
                        .OrderBy(x => x.Gname.Length)
                        .ThenBy(x => x.Gname)
                        .ToList();

            var group_list = data.Select(x => new { x.Gid, x.Gname }).Distinct()
                                .Select(g=> new ViewModels.StudentManagement.Group
                                {
                                    GroupId = g.Gid,
                                    GroupName = g.Gname,
                                    Students = data.Where(c => c.Gid == g.Gid)
                                                .Select(d => new ViewModels.StudentManagement.Student
                                                {
                                                    StudentId = d.StudentId,
                                                    StudentName = d.StudentName,
                                                    StudentSex = d.Sex,
                                                    IsLeader = d.IsLeader
                                                }).ToList()
                                })
                                .ToList();

            var vm = new GroupViewModel
            {
                CourseId = course.Cid,
                CourseName = course.Cname,
                StudentList = new MultiSelectList(student_list, "StudentId", "StudentName").ToList(),
                Groups = group_list,
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

        [HttpPost]
        public IActionResult ChangeLeader(string cid, string checkString)
        {
            var gid = _context.Students.Find(checkString).GroupId;
            var groupStu = _context.Students.Where(s => s.GroupId == gid).ToList();
            var oldLeader = groupStu.Find(s => s.IsLeader == true);

            // 更動 EvaluationCoaching 的 AUID and BUID 
            var evaluationCoaching_AUID = _context.EvaluationCoachings.Where(x => x.AUID == oldLeader.StudentId);
            if (evaluationCoaching_AUID != null)
            {
                foreach (var item in evaluationCoaching_AUID)
                {
                    var ec = new EvaluationCoaching()
                    {
                        AUID = checkString,
                        BUID = item.BUID,
                        MissionId = item.MissionId,
                        Evaluation = item.Evaluation,
                        Coaching = item.Coaching,
                    };
                    _context.EvaluationCoachings.Add(ec);
                    _context.EvaluationCoachings.Remove(item);
                }
                _context.SaveChanges();
            }
            var evaluationCoaching_BUID = _context.EvaluationCoachings.Where(x => x.BUID == oldLeader.StudentId);
            if (evaluationCoaching_BUID != null)
            {
                foreach (var item in evaluationCoaching_BUID)
                {
                    var ec = new EvaluationCoaching()
                    {
                        AUID = item.AUID,
                        BUID = checkString,
                        MissionId = item.MissionId,
                        Evaluation = item.Evaluation,
                        Coaching = item.Coaching,
                    };
                    _context.EvaluationCoachings.Add(ec);
                    _context.EvaluationCoachings.Remove(item);
                }
                _context.SaveChanges();
            }

            // 更動 Answer 的 UserID
            var answer = _context.Answers.Where(x => x.UserId == oldLeader.StudentId);
            if (answer != null)
            {
                foreach (var item in answer)
                {
                    var ans = new Answer();
                    ans = item;
                    ans.UserId = checkString;
                    _context.Answers.Add(ans);
                    _context.Answers.Remove(item);
                    _context.SaveChanges();
                }
            }
            

            groupStu.ForEach(s => s.IsLeader = false);
            _context.SaveChanges();
            groupStu.Find(s => s.StudentId == checkString).IsLeader = true;
            _context.SaveChanges();

            return RedirectToAction("Group", "StudentManagement", new { cid = cid });
        }

        // 刪除學生頁面 DeleteStudent
        public IActionResult DeleteStudent(string sid, string cid)
        {
            var student = _context.Students.Find(sid);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "StudentManagement", new { cid = cid });
        }
    }
}
