using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Server.Interfaces;
using Server.DTOs;
using Server.Entities;
using AutoMapper;
using Server.Helpers;
using Server.Params;

namespace Server.Data
{
    public class EformRepo : IEformRepo
    {
        private readonly PorticoContext _context;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public EformRepo(PorticoContext context, IMapper mapper, HttpClient httpClient)
        {
            _context = context;
            _mapper = mapper;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://hondaati1.mpidom.mpi/");
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Bearer c91fba83-6ad9-4d8b-9a72-f2db322efe88");
        }

        public async Task<PageList<EformDto>> GetEformListAsync(PageParams pageParams)
        {
            var query = 
                from eforms in _context.TbEformSessionForms
                where eforms.LogInfo!.EndsWith("PM") || eforms.LogInfo!.EndsWith("AM")
                select new EformDto 
                {
                    Id = eforms.EFormSessionFormId,
                    CreatedDate = eforms.CreatedDate,
                    LastModifiedDate = eforms.LastModifiedDate,
                    isSubmitted = eforms.IsSubmitted,
                    Type = "",
                    Log = eforms.LogInfo
                };

            query = query.AsQueryable();
            query = query.OrderByDescending(x => x.CreatedDate);

            return await PageList<EformDto>.CreateAsync(
                query,
                pageParams.PageNumber,
                pageParams.PageSize
            );
        }

        public async Task<EformResponse> GetUserListAsync()
        {
            var eformUsers = await _httpClient.GetFromJsonAsync<EformUsers>("manage/api/v1/admin/users");

            var userList = new List<EformUser>() { };
            foreach (var eformUser in eformUsers!.value!)
            {
                userList.Add(new EformUser()
                {
                    Id = eformUser.id,
                    Username = eformUser.username,
                    FirstName = eformUser.firstName,
                    LastName = eformUser.lastName,
                    Disabled = eformUser.disabled
                });
            }

            return await Task.Run(() =>
            {
                return new EformResponse()
                {
                    Data = userList,
                    NextLink = eformUsers.NextLink
                };
            });
        }

        public async Task<EformUserDto> GetUserAsync(string id)
        {
            var eformUser = new EformUserDto();
            try
            {
                eformUser = await _httpClient.GetFromJsonAsync<EformUserDto>("manage/api/v1/admin/users/" + id);
            }
            catch
            {
                eformUser = null;
            }
            return eformUser!;
        }

        public async Task<EformUserDeleteDto> DeleteUserAsync(string id)
        {
            var eformUser = await GetUserAsync(id);
            if (eformUser == null)
            {
                return new EformUserDeleteDto
                {
                    IsSuccess = false,
                    Message = "cannot find user with id. " + id,
                    EformUser = null
                };
            }

            var url = "manage/api/v1/admin/users/" + id;
            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return new EformUserDeleteDto
                {
                    IsSuccess = true,
                    Message = "deleted user with id. " + id,
                    EformUser = eformUser
                };
            }
            return new EformUserDeleteDto
            {
                IsSuccess = false,
                Message = "failed to delete user with id. " + id,
                EformUser = eformUser
            };
        }

        public async Task<List<EformDocDto>> GetDocs()
        {
            string path = "\\\\mpifilesrv01\\Environments\\Production\\EForms\\Done";

            var files = Directory.GetFiles(path)
                .Select(f => new FileInfo(f))
                .Where(f => f.LastAccessTime > DateTime.Now.Date.AddDays(-1))
                .ToList();

            var dates = new List<EformDocDto>();
            foreach (var file in files)
            {
                var fileNameArr = file.Name.Split("_");
                var dateStr = fileNameArr[fileNameArr.Length - 1];
                dateStr = dateStr.Split(".pdf")[0];

                string year = "2022";
                string monthday = dateStr.Split(year)[0];
                string month = monthday.Substring(0, 1);
                string day = monthday.Length > 2 ? monthday.Substring(1, 2) : monthday.Substring(1, 1);
                string time = dateStr.Split(year)[1];
                string hourminute = time.Split(" ")[0];
                string minute = hourminute.Substring(hourminute.Length - 2, 2);
                string hour = hourminute.Length > 3 ? hourminute.Substring(0, 2) : "0" + hourminute.Substring(0, 1);
                string ampm = time.Split(" ")[1];

                dateStr = year + "-" + month + "-" + day + " " + hour + ":" + minute + " " + ampm;
                var date = DateTime.Parse(dateStr);
                dateStr = date.ToString("yyyy-MM-dd HH:mm");

                var fileDto = new EformDocDto()
                {
                    Name = file.Name,
                    DateStr = dateStr
                };
                dates.Add(fileDto);
            }

            return await Task.Run(() =>
            {
                return dates.OrderByDescending(x => x.DateStr).ToList();
            });
        }
    }
}