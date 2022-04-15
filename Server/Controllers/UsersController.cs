using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;

namespace Server.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet("Self")]
        public async Task<ActionResult<UserDto>> GetSelf()
        {
            return await Task.Run(() => {
                String name = WindowsIdentity.GetCurrent().Name.ToString();
                var user = new UserDto() { Name = name };
                return user;
            });
        }

        [HttpGet("Self/Groups/List")]
        public async Task<ActionResult<List<string>>> GetGroups()
        {
            return await Task.Run(() => {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                var groupNames = from id in identity.Groups
                 select id.Translate(typeof(NTAccount)).Value;
                return Ok(groupNames);
            });
        }
    }
}