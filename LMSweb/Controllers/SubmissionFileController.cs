using LMSweb.Data;
using LMSweb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    public class SubmissionFileController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly LMSContext _context;

        public SubmissionFileController(ILogger<StudentController> logger, LMSContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index(string mid)
        {
            var vm = new SubmissionFileViewModel();

            return View();
        }
    }
}
