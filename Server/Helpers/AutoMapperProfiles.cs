using AutoMapper;
using Server.DTOs;
using Server.Entities;

namespace Server.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Appeal, AppealListDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.AppealId))
                .ForMember(d => d.Dept, o => o.MapFrom(s => s.Department.DepartmentCode))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.AppealContacts.FirstOrDefault()!.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.AppealContacts.FirstOrDefault()!.LastName))
                .ForMember(d => d.Meeting, o => o.MapFrom(s => s.AppealStatusLogs.FirstOrDefault()!.MeetingSchedule.MeetingDate))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.AppealStatusLogs.FirstOrDefault()!.AppealStatusType.AppealStatusTypeDescription))
                .ForMember(d => d.Notes, o => o.MapFrom(s => s.AppealStatusLogs.FirstOrDefault()!.Notes))
                .ForMember(d => d.StatusUpdateDate, o => o.MapFrom(s => s.AppealStatusLogs.FirstOrDefault()!.CreateDate))
                .ForMember(d => d.StatusUpdateUser, o => o.MapFrom(s => s.AppealStatusLogs.FirstOrDefault()!.CreateUser))
                .ForMember(d => d.ReceivedDate, o => o.MapFrom(s => s.AppealReceivedDate));
        }
    }
}