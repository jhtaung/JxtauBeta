namespace Server.DTOs
{
    public class EformUserDto
    {
        public string id { get; set; } = "";
        public string username { get; set; } = "";
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public bool disabled { get; set; }
        public string theme { get; set; } = "";
        public bool changePasswordAtNextLogin { get; set; }
        public bool passwordNeverExpires { get; set; }
        public bool lockedOut { get; set; }
        public bool twoFactorAuth { get; set; }
        public string email { get; set; } = "";
        public string culture { get; set; } = "";
        public string language { get; set; } = "";
        public string timezone { get; set; } = "";
        public string prefix { get; set; } = "";
        public string jobTitle { get; set; } = "";
        public string organization { get; set; } = "";
        public string phone { get; set; } = "";
        public string fax { get; set; } = "";
        public string pictureUrl { get; set; } = "";
        public StreetAddress streetAddress { get; set; } = new StreetAddress();
        public PostalAddress postalAddress { get; set; } = new PostalAddress();
        public bool strictAccessibility { get; set; }
        public CustomFields customFields { get; set; } = new CustomFields();
        public List<string> roles { get; set; } = new List<string>();
        public List<string> groups { get; set; } = new List<string>();
    }
    
    public class StreetAddress
    {
        public string line1 { get; set; } = "";
        public string line2 { get; set; } = "";
        public string city { get; set; } = "";
        public string state { get; set; } = "";
        public string zipcode { get; set; } = "";
        public string country { get; set; } = "";
    }

    public class PostalAddress
    {
        public string line1 { get; set; } = "";
        public string line2 { get; set; } = "";
        public string city { get; set; } = "";
        public string state { get; set; } = "";
        public string zipcode { get; set; } = "";
        public string country { get; set; } = "";
    }

    public class CustomFields
    {
        public string EmpID { get; set; } = "";
        public string MPIID { get; set; } = "";
    }
}
