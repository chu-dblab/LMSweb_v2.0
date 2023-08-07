using Microsoft.AspNetCore.Mvc;
using LMSweb.ViewModels.Nav;

namespace LMSweb.Controllers.Nav
{
    public class NavPartial : Controller
    {
        public ActionResult _TeacherNavPartial(TeacherNavViewModel vm)
        {
            return PartialView(vm);
        }
    }
}
