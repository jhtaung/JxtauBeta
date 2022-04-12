using Microsoft.Net.Http.Headers;
using Server.Interfaces;
using Server.Models;

namespace Server.Data
{
    public class EformRepo : IEformRepo
    {
        private readonly HttpClient _httpClient;
        public EformRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://hondaati1.mpidom.mpi/");
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Bearer c91fba83-6ad9-4d8b-9a72-f2db322efe88");
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

            return await Task.Run(() => {
                return new EformResponse()
                {
                    Data = userList,
                    NextLink = eformUsers.NextLink
                };
            });
        }
    }
}