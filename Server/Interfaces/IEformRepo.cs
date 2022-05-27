using Server.DTOs;
using Server.Helpers;
using Server.Params;

namespace Server.Interfaces
{
    public interface IEformRepo
    {
        Task<PageList<EformDto>> GetEformListAsync(PageParams pageParams);
        Task<EformResponse>GetUserListAsync();
        Task<EformUserDto>GetUserAsync(string id);
        Task<EformUserDeleteDto>DeleteUserAsync(string id);
        Task<List<EformDocDto>>GetDocs();
    }
}