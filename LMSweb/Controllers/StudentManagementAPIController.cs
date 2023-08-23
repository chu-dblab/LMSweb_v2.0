using LMSweb.Data;
using LMSweb.Services;
using LMSweb.ViewModels.StudentManagement;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Route("api/[Action]")]
    [ApiController]
    public class StudentManagementAPIController : ControllerBase
    {
        private readonly LMSContext _context;
        private readonly GroupServices _groupService;

        public StudentManagementAPIController(LMSContext context, GroupServices groupService)
        {
            _context = context;
            _groupService = groupService;
        }

        // api/StudentManagementAPI/GroupRandomCreate
        [HttpPost]
        public IActionResult GroupRandomCreate(int RandomNumber, string cid)
        {
            var GroupRandomCreateVM = new GroupRandomCreateViewModel();
            //var GroupSer = new GroupServices(_context);
            //GroupRandomCreateVM = GroupRandomCreateServices.GetGroupRandomCreateVM();

            _groupService.GetGroupRandomCreateVM(RandomNumber, cid);

            if (GroupRandomCreateVM == null)
            {
                return NotFound();
            }

            return Ok();
            //return Ok(GroupRandomCreateVM);
        }
    }
}
