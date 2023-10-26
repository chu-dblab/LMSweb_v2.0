using LMSweb.Data;
using LMSweb.Services;
using LMSweb.ViewModels.StudentManagement;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Route("api/[Action]")]
    [ApiController]
    public class StudentManagementAPIController : ControllerBase
    {
        private readonly LMSContext _context;
        private readonly GroupServices _groupService;

        public StudentManagementAPIController(LMSContext context, GroupServices groupService)
        {
            _context = context;
            _groupService = groupService;
        }

        // api/GroupRandomCreate
        [HttpPost]
        public IActionResult GroupRandomCreate(int RandomNumber, string cid)
        {
            var GroupRandomCreateVM = new GroupRandomCreateViewModel();
            
            _groupService.GetGroupRandomCreateVM(RandomNumber, cid);

            if (GroupRandomCreateVM == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // api/AddStudentToOtherGroup
        // 將學生加入其他組別
        [HttpPost]
        public IActionResult AddStudentToOtherGroup(AddStudentToOtherGroupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var sid in vm.StudentList)
                {

                    var student = _context.Students.Find(sid);
                    var group = _context.Groups.Find(vm.gid);
                    
                    if (student != null && group != null)
                    {
                        student.Group = group;
                    }
                }

                _context.SaveChanges();

                return Ok();
            }

            return Ok();
        }

        // api/GroupDelete
        // 刪除組別
        [HttpPost]
        public IActionResult GroupDelete(int gid)
        {
            var group = _context.Groups.Find(gid);

            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
            }

            return Ok();
        }

        // api/GroupStudentDelete
        // 刪除組別中的學生
        [HttpPost]
        public IActionResult GroupStudentDelete(string sid)
        {
            var student = _context.Students.Find(sid);

            if (student != null)
            {
                student.Group = null;
                student.GroupId = null;
                _context.SaveChanges();
            }

            return Ok();
        }

        // api/AddStudentToNewGroup
        // 創建一個組別並加入學生
        [HttpPost]
        public IActionResult AddStudentToNewGroup(AddStudentToNewGroupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var group = new Models.Group
                {
                    Gname = vm.GroupName,
                    CourseId = vm.CourseId,
                };

                _context.Groups.Add(group);
                _context.SaveChanges();

                foreach (var sid in vm.StudentList)
                {
                    var student = _context.Students.Find(sid);

                    if (student != null)
                    {
                        student.Group = group;
                    }
                }

                _context.SaveChanges();

                return Ok();
            }

            return Ok();
        }
    }
}
