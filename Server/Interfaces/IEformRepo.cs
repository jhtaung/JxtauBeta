using Server.Models;

namespace Server.Interfaces
{
    public interface IEformRepo
    {
        Task<EformResponse>GetUserListAsync();
    }
}