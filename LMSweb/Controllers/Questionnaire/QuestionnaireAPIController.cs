using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMSweb.ViewModels.Questionnaire;
using LMSweb.Services;
using LMSweb.Data;
using System.Diagnostics;

namespace LMSweb.Controllers.Questionnaire
{
    [Route("api/Questionnaire")]
    [ApiController]
    public class QuestionnaireAPIController : ControllerBase
    {
        private readonly LMSContext _context;

        public QuestionnaireAPIController(LMSContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //// Get: api/Questionnaire
        //public IActionResult Get(int tasktype, int tasksteps)
        //{
        //    var ReGetVM = new ReGetViewModel();
        //    var EprocedureSercices = new EprocedureSercices(_context);
        //    var EprocedureId = EprocedureSercices.GetEprocedureId(tasktype, tasksteps);

        //    if (EprocedureId == "D" || EprocedureId == "C")
        //    {
        //        return NotFound();
        //    }
        //    var Topic = EprocedureSercices.GetEprocedureContent(EprocedureId);

        //    if (Topic == null)
        //    {
        //        return NotFound();
        //    }
        //    ReGetVM.Topic = Topic;
        //    return Ok(ReGetVM);
        //}

        public IActionResult Get(int tasktype, int tasksteps, string? groupLeaderId)
        {
            var ReGetVM = new ReGetViewModel();
            var EprocedureSercices = new EprocedureSercices(_context);
            var EprocedureId = EprocedureSercices.GetEprocedureId(tasktype, tasksteps);

            if (EprocedureId == "D" || EprocedureId == "C")
            {
                return NotFound();
            }
            var Topic = EprocedureSercices.GetEprocedureContent(EprocedureId);

            if (Topic == null)
            {
                return NotFound();
            }
            ReGetVM.Topic = Topic;

            if(groupLeaderId != null)
            {
                var _Evaluation = new LMSweb.ViewModels.Questionnaire.Evaluation();

                var gL = _context.Students.Find(groupLeaderId);
                var groupId = _context.Students.Find(groupLeaderId).GroupId;

                _Evaluation.GroupId = groupId.ToString();
                _Evaluation.GroupLeaderId = groupLeaderId;

                var D_url = (from ec in _context.ExecutionContents
                             from s in _context.Students
                             where s.StudentId == groupLeaderId && ec.GroupId == s.GroupId && ec.Type == "D"
                             select new
                             {
                                 DrawingUrl = ec.Path,
                             }).FirstOrDefault();

                var C_url = (from ec in _context.ExecutionContents
                             from s in _context.Students
                             where s.StudentId == groupLeaderId && ec.GroupId == s.GroupId && ec.Type == "C"
                             select new
                             {
                                 CodingUrl = ec.Path,
                             }).FirstOrDefault();

                _Evaluation.DrawingUrl = "UploadImgs/" + D_url.DrawingUrl;
                _Evaluation.CodingUrl = "UploadImgs/" + C_url.CodingUrl;

                ReGetVM.Evaluation = _Evaluation;
            }

            return Ok(ReGetVM);
        }

        // POST: api/Questionnaire
        [HttpPost]
        public IActionResult Post([FromBody] PostViewModel vm)
        {
            var EprocedureSercices = new EprocedureSercices(_context);
            if (vm == null) { return NotFound(); }
            else
            {
                if(vm.EprocedureId == null)
                {
                    EprocedureSercices.SaveAnswer(vm);
                }
                else if(vm.EprocedureId == "6")
                {
                    EprocedureSercices.SaveAnswerByEvaluation(vm);
                    //Debug.WriteLine(vm);
                }
               
                return Ok();
            }
        }
    }
}
