using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    public class AllPartial : Controller
    {
        public IActionResult _ViewTitlePartial()
        {
            return View();
        }
    }
}
