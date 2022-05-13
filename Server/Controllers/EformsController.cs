using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.DTOs;

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

        [HttpGet("Users/{id}")]
        public async Task<ActionResult<EformUserDto>> GetUser(string id)
        {
            var eformUser = await _unitOfWork.EformRepo.GetUserAsync(id);
            return Ok(eformUser);
        }

        [HttpDelete("Users/{id}")]
        public async Task<ActionResult<EformUserDeleteDto>> DeleteUser(string id)
        {
            var eformUserDeleteDto = await _unitOfWork.EformRepo.DeleteUserAsync(id);
            return eformUserDeleteDto.IsSuccess ? Ok(eformUserDeleteDto) : BadRequest(eformUserDeleteDto);
        }

        [HttpGet("Docs")]
        public async Task<ActionResult<List<EformDocDto>>> GetAxDocs()
        {
            var axDocs = await _unitOfWork.EformRepo.GetDocs();
            return Ok(axDocs);
        }
    }
}