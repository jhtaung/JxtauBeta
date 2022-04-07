using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppealStatusLog
    {
        /// <summary>
        /// Unique Identifier for the appeal status log
        /// </summary>
        public int AppealStatusLogId { get; set; }
        /// <summary>
        /// Identifier of the appeal
        /// </summary>
        public int AppealId { get; set; }
        /// <summary>
        /// Unique identifier for the meeting the appeal will be heard
        /// </summary>
        public int MeetingScheduleId { get; set; }
        /// <summary>
        /// Current status of the appeal
        /// </summary>
        public int AppealStatusTypeId { get; set; }
        /// <summary>
        /// Notes about the status
        /// </summary>
        public string? Notes { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual Appeal Appeal { get; set; } = null!;
        public virtual AppealStatusType AppealStatusType { get; set; } = null!;
        public virtual MeetingSchedule MeetingSchedule { get; set; } = null!;
    }
}
