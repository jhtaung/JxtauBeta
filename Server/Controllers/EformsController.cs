using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    public class EformsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EformsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Users/List")]
        public async Task<ActionResult<EformResponse>> GetUserList()
        {
            var eformResponse = await _unitOfWork.EformRepo.GetUserListAsync();
            return Ok(eformResponse);
        }
    }
}