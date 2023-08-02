using Microsoft.AspNetCore.Http;
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

        public QuestionnaireAPIController(LMSContext context)
        {
            _context = context;
        }

        [HttpPost]
        // Get: api/Questionnaire
        public IActionResult Get([FromBody] GetViewModel VM)
        {
            var ReGetVM = new ReGetViewModel();
            var EprocedureSercices = new EprocedureSercices(_context);

            var EprocedureId = EprocedureSercices.GetEprocedureId(VM.TaskType, VM.TaskSteps);
            
            if (EprocedureId == "D" || EprocedureId == "C")
            {
                return NotFound();
            }
            else
            {
                var Topic = EprocedureSercices.GetEprocedureContent(EprocedureId);

                if (Topic != null)
                {
                    ReGetVM.Topic = Topic;
                }
                else
                {
                    return NotFound();
                }
            }

            

            return Ok(ReGetVM);
        }
        //// POST: api/Questionnaire
        //[HttpPost]
        //public void Post([FromBody] PostViewModel VM)
        //{
        //}
    }
}
