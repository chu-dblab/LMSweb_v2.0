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

        [HttpGet]
        // Get: api/Questionnaire
        public IActionResult Get(int tasktype, int tasksteps)
        {
            var ReGetVM = new ReGetViewModel();
            var EprocedureSercices = new EprocedureSercices(_context);
            var EprocedureId = EprocedureSercices.GetEprocedureId(tasktype,tasksteps);

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
            return Ok(ReGetVM);
        }

        // POST: api/Questionnaire
        [HttpPost]
        public IActionResult Post([FromBody] PostViewModel VM)
        {
            if (VM == null) { return NotFound(); }
            else
            {
                var EprocedureSercices = new EprocedureSercices(_context);
                EprocedureSercices.SaveAnswer(VM);

                return Ok();
            }
        }
    }
}
