using Server.DTOs;

namespace Server.Interfaces
{
    public interface IEformRepo
    {
        Task<EformResponse>GetUserListAsync();
        Task<EformUserDto>GetUserAsync(string id);
        Task<string>DeleteUserAsync(string id);
        Task<List<EformDocDto>>GetDocs();
        
    }
}