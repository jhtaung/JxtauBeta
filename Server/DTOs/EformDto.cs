namespace Server.DTOs
{
    public class EformDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool isSubmitted { get; set; }
        public string? Type { get; set; }
        public string? Log { get; set; }
    }
}