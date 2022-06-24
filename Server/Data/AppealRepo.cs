using AutoMapper;
using LinqKit;
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
                   Notes = status.Notes ?? "",
                   StatusUpdateUser = status.UpdateUser,
                   StatusUpdateDate = status.UpdateDate,
                   ReceivedDate = appeal.AppealReceivedDate
               };

            query = query.AsQueryable();
            query = query.OrderByDescending(x => x.Id);

            query = GetListQuery(appealParams, query);

            var sortHelper = new SortHelper<AppealListDto>();
            query = sortHelper.ApplySort(query, appealParams.OrderBy);

            return await PageList<AppealListDto>.CreateAsync(
                query,
                appealParams.PageNumber,
                appealParams.PageSize
            );
        }

        private IQueryable<AppealListDto> GetListQuery(
            AppealParams appealParams,
            IQueryable<AppealListDto> query
        )
        {
            if (appealParams.Id != null)
            {
                query = query.Where(x => x.Id == appealParams.Id);
            }

            if (appealParams.Rap != null)
            {
                query = query.Where(x => x.Rap == appealParams.Rap);
            }

            if (appealParams.Dept != null)
            {
                query = query.Where(x => x.Dept == appealParams.Dept);
            }

            if (appealParams.Mpid != null)
            {
                query = query.Where(x => x.Mpid == appealParams.Mpid);
            }

            if (appealParams.MpidContains != null)
            {
                query = query.Where(x => x.Mpid.Contains(appealParams.MpidContains));
            }

            if (appealParams.MpidMultiple != null)
            {
                List<string> mpidMultiple = appealParams.MpidMultiple.Split(",").ToList();
                foreach (var mpid in mpidMultiple)
                {
                    query = query.Where(x => x.Mpid == mpid);
                }
            }

            if (appealParams.FirstName != null)
            {
                query = query.Where(x => x.FirstName == appealParams.FirstName);
            }

            if (appealParams.LastName != null)
            {
                query = query.Where(x => x.LastName == appealParams.LastName);
            }

            if (appealParams.Status != null)
            {
                query = query.Where(x => x.Status == appealParams.Status);
            }

            if (appealParams.Notes != null)
            {
                query = query.Where(x => x.Notes == appealParams.Notes);
            }

            if (appealParams.NotesContains != null)
            {
                query = query.Where(x => x.Notes.Contains(appealParams.NotesContains));
            }

            if (appealParams.StatusUpdateUser != null)
            {
                query = query.Where(x => x.StatusUpdateUser == appealParams.StatusUpdateUser);
            }

            if (appealParams.opt!.ToLower() == "all")
            {
                var searchHelper = new SearchHelper<AppealListDto>();
                query = searchHelper.ApplySearch(query, appealParams.Search);

                // string search = appealParams.Search;
                // var listAnd = search.Split("AND").ToList();
                // var listOr = search.Split("OR").ToList();

                var predicate = PredicateBuilder.New<AppealListDto>();
                predicate.And(x => x.Rap == appealParams.Rap);
            }

            return query;
        }
    }
}