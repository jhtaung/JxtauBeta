namespace Server.DTOs
{
    public class EformUserDeleteDto
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
        public EformUserDto? EformUser { get; set; }
    }
}