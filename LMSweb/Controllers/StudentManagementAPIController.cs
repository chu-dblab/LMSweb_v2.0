using LMSweb.Data;
using LMSweb.Services;
using LMSweb.Views.StudentManagement;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Route("api/[Action]")]
    [ApiController]
    public class StudentManagementAPIController : ControllerBase
    {
        private readonly LMSContext _context;

        public StudentManagementAPIController(LMSContext context)
        {
            _context = context;
        }

        // api/StudentManagementAPI/GroupRandomCreate
        [HttpPost]
        public IActionResult GroupRandomCreate(int RandomNumber, string cid)
        {
            var GroupRandomCreateVM = new GroupRandomCreateViewModel();
            var GroupSer = new GroupServices(_context);
            //GroupRandomCreateVM = GroupRandomCreateServices.GetGroupRandomCreateVM();

            GroupSer.GetGroupRandomCreateVM(RandomNumber, cid);

            if (GroupRandomCreateVM == null)
            {
                return NotFound();
            }

            return Ok();
            //return Ok(GroupRandomCreateVM);
        }
    }
}
