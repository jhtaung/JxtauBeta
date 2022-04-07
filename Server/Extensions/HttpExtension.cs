using System.Text.Json;
using Server.Helpers;

namespace Server.Extensions
{
    public static class HttpExtension
    {
        public static void AddPageHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var pageHeader = new PageHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(pageHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}