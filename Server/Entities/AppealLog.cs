using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppealLog
    {
        public int AppealLogId { get; set; }
        public string? Description { get; set; }
        public string ProcessName { get; set; } = null!;
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
