using AutoMapper;
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
                let status = _context.AppealStatusLogs
                    .Where(x => x.AppealId == appeal.AppealId)
                    .OrderByDescending(x => x.AppealStatusLogId)
                    .FirstOrDefault()
                join statusType in _context.AppealStatusTypes
                    on status.AppealStatusTypeId equals statusType.AppealStatusTypeId
                join department in _context.Departments
                    on appeal.DepartmentId equals department.DepartmentId
                join meeting in _context.MeetingSchedules 
                    on status.MeetingScheduleId equals meeting.MeetingScheduleId
                join contacts in _context.AppealContacts.Where(x => x.ContactTypeId == 1)
                    on appeal.AppealId equals contacts.AppealId into leftContacts
                from contact in leftContacts.DefaultIfEmpty()
                orderby appeal.AppealId descending
                select new AppealListDto
                {
                    Id = appeal.AppealId,
                    Rap = appeal.Rap,
                    Dept = department.DepartmentCode,
                    Mpid = appeal.Mpid,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Meeting = (meeting.MeetingTime ?? meeting.MeetingDate),
                    Status = statusType.AppealStatusTypeDescription,
                    Notes = status.Notes,
                    StatusUpdateUser = status.UpdateUser,
                    StatusUpdateDate = status.UpdateDate,
                    ReceivedDate = appeal.AppealReceivedDate
                };
            
            query = query.AsQueryable();
            query = query.OrderByDescending(x => x.Id); 

            var sortHelper = new SortHelper<AppealListDto>();
            query = sortHelper.ApplySort(query, appealParams.OrderBy);

            return await PageList<AppealListDto>.CreateAsync(
                query,
                appealParams.PageNumber, 
                appealParams.PageSize
            );
        }
    }
}