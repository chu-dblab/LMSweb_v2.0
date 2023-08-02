using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMSweb.ViewModels.Questionnaire;

namespace LMSweb.Controllers.Questionnaire
{
    [Route("api/Questionnaire")]
    [ApiController]
    public class QuestionnaireAPIController : ControllerBase
    {
        [HttpGet]
        // POST: api/Questionnaire
        public string Get([FromBody] GetViewModel VM)
        {
            return "value";
        }
        // 
        // POST: api/Questionnaire
        [HttpPost]
        public void Post([FromBody] PostViewModel VM)
        {
        }
    }
}
