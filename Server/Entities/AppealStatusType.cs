using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppealStatusType
    {
        public AppealStatusType()
        {
            AppealStatusLogs = new HashSet<AppealStatusLog>();
        }

        /// <summary>
        /// Unique identifier of the appeal status type
        /// </summary>
        public int AppealStatusTypeId { get; set; }
        /// <summary>
        /// Descirption of the status e.g. Pending, Resolved, Tabled
        /// </summary>
        public string AppealStatusTypeDescription { get; set; } = null!;
        /// <summary>
        /// Active/Inactive indicator of the status type
        /// </summary>
        public short IsActive { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<AppealStatusLog> AppealStatusLogs { get; set; }
    }
}
