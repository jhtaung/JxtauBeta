using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.Helpers;
using Server.Interfaces;
using Server.Params;

namespace Server.Data
{
    public class AppealRepo : IAppealRepo
    {
        private readonly AppealContext _context;
        private readonly IMapper _mapper;
        public AppealRepo(AppealContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PageList<AppealListDto>> GetListAsync(AppealParams appealParams)
        {
            var query = 
                from appeal in _context.Appeals
                from log in _context.AppealStatusLogs
                    .Where(x => x.AppealId == appeal.AppealId)
                    .OrderByDescending(x => x.AppealId)
                    .Take(1)
                    .DefaultIfEmpty()
                from meeting in _context.MeetingSchedules
                    .Where(x => x.MeetingScheduleId == log.MeetingScheduleId)
                    .DefaultIfEmpty()
                from plan in _context.PlanTypes
                    .Where(x => x.PlanTypeId == appeal.PlanTypeId)
                    .DefaultIfEmpty()
                from department in _context.Departments
                    .Where(x => x.DepartmentId == appeal.DepartmentId)
                    .DefaultIfEmpty()
                from contact in _context.AppealContacts
                    .Where(x => x.ContactTypeId == 1)
                    .Where(x => x.AppealId == appeal.AppealId)
                    .DefaultIfEmpty()
                from contactType in _context.ContactTypes
                    .Where(x => x.ContactTypeId == contact.ContactTypeId)
                    .DefaultIfEmpty()
                select appeal;

            query = query.AsQueryable();

            query = query.OrderByDescending(x => x.AppealId);

            return await PageList<AppealListDto>.CreateAsync(
                query.ProjectTo<AppealListDto>(_mapper.ConfigurationProvider).AsNoTracking(),
                appealParams.PageNumber, 
                appealParams.PageSize
            );
        }
    }
}