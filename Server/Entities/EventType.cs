using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class EventType
    {
        public EventType()
        {
            AppealEvents = new HashSet<AppealEvent>();
        }

        /// <summary>
        /// Unique identifier for event type
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// Event type description: Note, Timeline, etc
        /// </summary>
        public string EventTypeDescription { get; set; } = null!;
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<AppealEvent> AppealEvents { get; set; }
    }
}
