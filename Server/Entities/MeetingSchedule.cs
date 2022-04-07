using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class MeetingSchedule
    {
        public MeetingSchedule()
        {
            AppealStatusLogs = new HashSet<AppealStatusLog>();
        }

        /// <summary>
        /// Unique identifier for meeting schedule
        /// </summary>
        public int MeetingScheduleId { get; set; }
        /// <summary>
        /// Meeting Date
        /// </summary>
        public DateTime MeetingDate { get; set; }
        /// <summary>
        /// meeting time
        /// </summary>
        public DateTime? MeetingTime { get; set; }
        /// <summary>
        /// Date the appeal memo is needed 
        /// </summary>
        public DateTime? CutOffDate { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<AppealStatusLog> AppealStatusLogs { get; set; }
    }
}
