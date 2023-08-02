using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers.Questionnaire
{
    public class QuestionnaireController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
