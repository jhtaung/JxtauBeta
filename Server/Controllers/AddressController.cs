using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Interfaces;
using Server.Params;

namespace Server.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Validate")]
        public async Task<ActionResult<AddressDto>> GetAddress(AddressParams addressParams)
        {
            return await _unitOfWork.AddressRepo.GetAddressAsync(addressParams);
        }

        [HttpPost("ValidateZip")]
        public async Task<ActionResult<AddressZipDto>> GetAddressZip(AddressZipParams addressZipParams)
        {
            return await _unitOfWork.AddressRepo.GetAddressZipAsync(addressZipParams);
        }
    }


}