using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Extensions;
using Server.Interfaces;
using Server.Params;

namespace Server.Controllers
{
    public class AppealsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppealsController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<AppealListDto>>> GetList([FromQuery]AppealParams appealParams)
        {
            var appeals = await _unitOfWork.AppealRepo.GetListAsync(appealParams);
            Response.AddPageHeader(appeals.CurrentPage, appeals.PageSize, appeals.TotalCount, appeals.TotalPages);
            return Ok(appeals);
        }
    }
}