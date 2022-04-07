using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppellantType
    {
        public AppellantType()
        {
            AppealContacts = new HashSet<AppealContact>();
        }

        /// <summary>
        /// Unique identifier for the appellant type
        /// </summary>
        public int AppellantTypeId { get; set; }
        /// <summary>
        /// Appellant type name
        /// </summary>
        public string? AppellantTypeName { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<AppealContact> AppealContacts { get; set; }
    }
}
