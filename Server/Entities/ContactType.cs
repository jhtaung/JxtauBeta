using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class ContactType
    {
        public ContactType()
        {
            AppealContacts = new HashSet<AppealContact>();
        }

        /// <summary>
        /// Unique identifier for contact type
        /// </summary>
        public int ContactTypeId { get; set; }
        /// <summary>
        /// Contact type name: appellant, attorney, etc
        /// </summary>
        public string ContactTypeName { get; set; } = null!;
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<AppealContact> AppealContacts { get; set; }
    }
}
