using Server.DTOs;
using Server.Entities;
using Server.Helpers;
using Server.Params;

namespace Server.Interfaces
{
    public interface IAppealRepo
    {
        Task<PageList<AppealListDto>> GetListAsync(AppealParams appealParams);
    }
}