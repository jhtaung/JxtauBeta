namespace Server.Params
{
    public class AppealParams : PageParams
    {
        public int? Id { get; set; }
        public bool? Rap { get; set; }
        public string? Dept { get; set; }
        public string? Mpid { get; set; }
        public string? MpidContains { get; set; }
        public string? MpidMultiple { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Meeting { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public string? NotesContains { get; set; }
        public string? StatusUpdateUser { get; set; }
        public DateTime? StatusUpdateDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? opt { get; set; }
    }
}