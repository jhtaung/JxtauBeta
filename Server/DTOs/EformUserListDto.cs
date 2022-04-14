using Newtonsoft.Json;

namespace Server.DTOs
{
    public class EformUsers
    {
        public List<Value>? value { get; set; }

        [JsonProperty("@nextLink")]
        public string? NextLink { get; set; }
    }

    public class Value
    {
        public string id { get; set; } = "";
        public string username { get; set; } = "";
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public bool disabled { get; set; }
    }

    public class EformResponse
    {
        public List<EformUser>? Data { get; set; }
        public string? NextLink { get; set; }
    }

    public class EformUser
    {
        public string Id { get; set; } = "";
        public string Username { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public bool Disabled { get; set; }
    }
}
