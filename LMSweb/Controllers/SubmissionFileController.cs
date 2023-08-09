﻿using LMSweb.Data;
using LMSweb.Models;
using LMSweb.Services;
using LMSweb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
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
            var MisstionId = _context.Executions.Where(x => x.GroupId == GroupId).FirstOrDefault().MissionId;
            var misstion = _context.Missions.Find(mid);

            vm.CourseName = _context.Courses.FirstOrDefault(x => x.Cid == _context.Students.Where(x => x.StudentId == UID).FirstOrDefault().CourseId).Cname;
            vm.MisstionId = MisstionId;
            vm.MisstionName = _context.Missions.FirstOrDefault(x => x.CourseId == _context.Students.Where(x => x.StudentId == UID).FirstOrDefault().CourseId).Mname;
            vm.EndDate = _context.Missions.FirstOrDefault(x => x.CourseId == _context.Students.Where(x => x.StudentId == UID).FirstOrDefault().CourseId).EndDate;
            
            if (UID == null || misstion == null) { return NotFound(); }

            var _ExecutionContent = _context.ExecutionContents.Where(x => x.GroupId == GroupId && x.MissionId == MisstionId && x.Type == type).FirstOrDefault();
            
            if(_ExecutionContent != null )
            {
                vm.Path = _ExecutionContent.Path;
            } 

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(string mid, string type, SubmissionFileViewModel vm)
        {
            if (vm.Path == null) { return NotFound(); }
             
            var UID = User.Claims.FirstOrDefault(x => x.Type == "UID");   //抓出當初記載Claims陣列中的TID

            if (UID == null) { return NotFound(); }

            var GroupId = _context.Students.Where(x => x.StudentId == UID.Value).FirstOrDefault().GroupId;
            var MisstionId = _context.Executions.Where(x => x.GroupId == GroupId).FirstOrDefault().MissionId;

            var path = _fileUploadService.SaveFile(vm.formFile, "UploadImgs");

            if(path == null) { return NotFound(); }

            var _ExecutionContent = new ExecutionContent()
            {
                MissionId = MisstionId,
                GroupId = GroupId ?? 0,
                Path = vm.formFile.FileName ?? "no fileName",
                Type = vm.type ?? "no type",
            };

            return View(vm);
        }
    }
}
