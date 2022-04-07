using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppealEvent
    {
        /// <summary>
        /// Unique identifier for Appeal Event
        /// </summary>
        public int AppealEventId { get; set; }
        /// <summary>
        /// ID of the appeal
        /// </summary>
        public int AppealId { get; set; }
        /// <summary>
        /// Type of event the data is for, Timeline, Note, etc
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// Date of the event
        /// </summary>
        public DateTime? EventDate { get; set; }
        /// <summary>
        /// Information about the event
        /// </summary>
        public string? EventText { get; set; }
        public string? PageNumber { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual Appeal Appeal { get; set; } = null!;
        public virtual EventType EventType { get; set; } = null!;
    }
}
