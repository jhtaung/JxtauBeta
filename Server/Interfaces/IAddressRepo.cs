using Server.DTOs;
using Server.Params;

namespace Server.Interfaces
{
    public interface IAddressRepo
    {
        Task<AddressDto>GetAddressAsync(AddressParams addressParams);
        Task<AddressZipDto>GetAddressZipAsync(AddressZipParams addressZipParams);
    }
}