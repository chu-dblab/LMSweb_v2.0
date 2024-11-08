using LMSweb.Data;
using LMSweb.Models;
using LMSweb.Services;
using LMSweb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Authorize]
    public class SubmissionFileController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly LMSContext _context;
        private readonly FileUploadService _fileUploadService;

        public SubmissionFileController(ILogger<StudentController> logger, LMSContext context, FileUploadService fileUploadService)
        {
            _logger = logger;
            _context = context;
            _fileUploadService = fileUploadService;
        }


        public IActionResult Index(string mid, string type)
        {
            var vm = new SubmissionFileViewModel();
            vm.type = type;
            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID").Value;   //抓出當初記載Claims陣列中的TID

            var GroupId = _context.Students.Where(x => x.StudentId == UID).FirstOrDefault().GroupId;
            var misstion = _context.Missions.Find(mid);
            var cid = _context.Students.Where(x => x.StudentId == UID).FirstOrDefault().CourseId;
            
            vm.CourseId = cid;
            vm.CourseName = _context.Courses.FirstOrDefault(x => x.Cid == _context.Students.Where(x => x.StudentId == UID).FirstOrDefault().CourseId).Cname;
            vm.MissionId = mid;
            vm.MisstionName = _context.Missions.Find(mid).Mname;
            vm.EndDate = _context.Missions.Find(mid).EndDate;
            vm.IsUpload = _context.Executions.Where(x => x.GroupId == GroupId && x.MissionId == mid).FirstOrDefault().IsPeerEvaluation;

            if (UID == null || misstion == null) { return NotFound(); }

            var _ExecutionContent = _context.ExecutionContents.Where(x => x.GroupId == GroupId && x.MissionId == mid && x.Type == type).FirstOrDefault();
            
            if(_ExecutionContent != null )
            {
                vm.Path = "UploadImgs/" + _ExecutionContent.Path;
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(string mid, string type, SubmissionFileViewModel vm)
        {
            //if (vm.Path == null) { return NotFound(); }
             
            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID");   //抓出當初記載Claims陣列中的TID

            if (UID == null) { return NotFound(); }

            var GroupId = _context.Students.Where(x => x.StudentId == UID.Value).FirstOrDefault().GroupId;
            var cid = _context.Students.Where(x => x.StudentId == UID.Value).FirstOrDefault().CourseId;

            if(vm.formFile == null)
            {
                return RedirectToAction("Index", "SubmissionFile", new { mid = mid, type = type });
            }

            var input_path = Path.GetExtension(vm.formFile.FileName);
            var fileExt = Path.GetExtension(input_path);
            var fileNewName = $@"{mid}{GroupId}{DateTime.Now.ToString("MMddHHmmss")}";

            var path = _fileUploadService.SaveFile(vm.formFile, "UploadImgs", fileNewName + fileExt);

            if(path == null) { return NotFound(); }

            var _ExecutionContent = new ExecutionContent()
            {
                MissionId = mid,
                GroupId = GroupId ?? 0,
                Path = fileNewName + fileExt ?? "no fileName",
                Type = vm.type ?? "no type",
            };

            var _ExecutionContentDB = _context.ExecutionContents.Where(x => x.GroupId == GroupId && x.MissionId == mid && x.Type == type).FirstOrDefault();
            if( _ExecutionContentDB == null )
            {
                _context.ExecutionContents.Add(_ExecutionContent);
            } 
            else
            {
                _ExecutionContentDB.Path = _ExecutionContent.Path;
            }

            _context.SaveChanges();


            // 重新修正 vm
            vm.CourseId = cid;
            vm.CourseName = _context.Courses.FirstOrDefault(x => x.Cid == _context.Students.Where(x => x.StudentId == UID.Value).FirstOrDefault().CourseId).Cname;
            vm.MissionId = mid;
            vm.MisstionName = _context.Missions.Find(mid).Mname;
            vm.EndDate = _context.Missions.FirstOrDefault(x => x.CourseId == _context.Students.Where(x => x.StudentId == UID.Value).FirstOrDefault().CourseId).EndDate;
            vm.type = type;
            vm.Path = "UploadImgs/" + _ExecutionContent.Path;

            return View(vm);
        }
    }
}
