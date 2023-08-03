using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
