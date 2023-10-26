using Microsoft.AspNetCore.Mvc;
using LMSweb.ViewModels.Questionnaire;
using LMSweb.Services;
using LMSweb.Data;

namespace LMSweb.Controllers.Questionnaire
{
    [Route("api/Questionnaire")]
    [ApiController]
    public class QuestionnaireAPIController : ControllerBase
    {
        private readonly LMSContext _context;
        private readonly EprocedureSercices _eprocedureSercices;
        private readonly EvaluationCoachingServices _evaluationCoachingServices;

        public QuestionnaireAPIController(LMSContext context, EprocedureSercices eprocedureSercices, EvaluationCoachingServices evaluationCoachingServices)
        {
            _context = context;
            _eprocedureSercices = eprocedureSercices;
            _evaluationCoachingServices = evaluationCoachingServices;
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

        public IActionResult Get(int tasktype, int tasksteps, string? groupLeaderId, string? mid, string? uid)
        {
            var ReGetVM = new ReGetViewModel();
            //var EprocedureSercices = new EprocedureSercices(_context);
            var EprocedureId = _eprocedureSercices.GetEprocedureId(tasktype, tasksteps);

            if (EprocedureId == "D" || EprocedureId == "C")
            {
                return NotFound();
            }
            var Topic = _eprocedureSercices.GetEprocedureContent(EprocedureId);

            if (Topic == null)
            {
                return NotFound();
            }
            ReGetVM.Topic = Topic;

            // 互評
            if(groupLeaderId != null)
            {
                if (EprocedureId == "6")
                {
                    var _Evaluation = new Evaluation();

                    var gL = _context.Students.Find(groupLeaderId);
                    var groupId = _context.Students.Find(groupLeaderId).GroupId;

                    _Evaluation.GroupId = groupId.ToString();
                    _Evaluation.GroupLeaderId = groupLeaderId;

                    var D_url = (from ec in _context.ExecutionContents
                                 from s in _context.Students
                                 where s.StudentId == groupLeaderId && ec.GroupId == s.GroupId && ec.Type == "D" && ec.MissionId == mid
                                 select new
                                 {
                                     DrawingUrl = ec.Path,
                                 }).FirstOrDefault();

                    var C_url = (from ec in _context.ExecutionContents
                                 from s in _context.Students
                                 where s.StudentId == groupLeaderId && ec.GroupId == s.GroupId && ec.Type == "C" && ec.MissionId == mid
                                 select new
                                 {
                                     CodingUrl = ec.Path,
                                 }).FirstOrDefault();

                    if(D_url == null)
                        _Evaluation.DrawingUrl = "";
                    else
                        _Evaluation.DrawingUrl = "UploadImgs/" + D_url.DrawingUrl;

                    if (C_url == null)
                        _Evaluation.CodingUrl = "";
                    else
                        _Evaluation.CodingUrl = "UploadImgs/" + C_url.CodingUrl;

                    ReGetVM.Evaluation = _Evaluation;

                    var sortQuestionsList = Topic.Questions.OrderBy(x => x.QuestionId.Substring(x.QuestionId.Length - 1, 1)).Where(x => x.Type == "2").ToList();

                    int type2Count = Topic.Questions.Where(x => x.Type == "2").Count();

                    for(int i = 0; i < type2Count; i++)
                    {
                        Topic.Questions[Topic.Questions.Count - type2Count + i] = sortQuestionsList[i];
                    }
                }
            }

            // 回饋
            if (EprocedureId == "7")
            {
                var _Coaching = new Coaching();

                if (mid != null && uid != null)
                    _Coaching = _evaluationCoachingServices.GetCoaching(mid, uid);

                ReGetVM.Coaching = _Coaching;

                var sortQuestionsList = Topic.Questions.OrderBy(x => x.QuestionId.Substring(x.QuestionId.Length - 1, 1)).Where(x => x.Type == "2").ToList();
                int type2Count = Topic.Questions.Where(x => x.Type == "2").Count();

                for (int i = 0; i < type2Count; i++)
                {
                    Topic.Questions[Topic.Questions.Count - type2Count + i] = sortQuestionsList[i];
                }
            }

            return Ok(ReGetVM);
        }

        // POST: api/Questionnaire
        [HttpPost]
        public IActionResult Post([FromBody] PostViewModel vm)
        {
            //var EprocedureSercices = new EprocedureSercices(_context);
            //var _EvaluationCoachingServices = new EvaluationCoachingServices(_context);

            if (vm == null) { return NotFound(); }
            else
            {
                if(vm.EprocedureId == null)
                {
                    _eprocedureSercices.SaveAnswer(vm);
                }
                else if(vm.EprocedureId == "6")
                {
                    _evaluationCoachingServices.SaveAnswerByEvaluation(vm);
                    //Debug.WriteLine(vm);
                }
                else if(vm.EprocedureId == "7")
                {
                    _evaluationCoachingServices.SaveAnswerByCoaching(vm);
                    //Debug.WriteLine(vm);
                }
               
                return Ok();
            }
        }
    }
}
