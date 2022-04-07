using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppealContact
    {
        /// <summary>
        /// Unique ID for Appeal Contact
        /// </summary>
        public int AppealContactId { get; set; }
        /// <summary>
        /// Contact Type ID indicating Provider, Appeallant, etc
        /// </summary>
        public int ContactTypeId { get; set; }
        /// <summary>
        /// Unique ID of the Appeal
        /// </summary>
        public int AppealId { get; set; }
        /// <summary>
        /// Type of Appellant Spouse, Child, etc
        /// </summary>
        public int? AppellantTypeId { get; set; }
        /// <summary>
        /// First Name of the Contact
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Middle Name of the appeal contact
        /// </summary>
        public string? MiddleName { get; set; }
        /// <summary>
        /// Last name of the appeal contact
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Company name for appeal contact
        /// </summary>
        public string? CompanyName { get; set; }
        /// <summary>
        /// Address Line 1 of appeal contact
        /// </summary>
        public string? AddressLine1 { get; set; }
        /// <summary>
        /// Address Line 2 of the appeal contact
        /// </summary>
        public string? AddressLine2 { get; set; }
        /// <summary>
        /// City of the appeal contact
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        /// State code of the appeal contact
        /// </summary>
        public string? State { get; set; }
        /// <summary>
        /// Zip code of the appeal contact
        /// </summary>
        public string? Zip { get; set; }
        /// <summary>
        /// Provider ID associated with the appeal
        /// </summary>
        public string? ProviderId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateUser { get; set; }

        public virtual Appeal Appeal { get; set; } = null!;
        public virtual AppellantType? AppellantType { get; set; }
        public virtual ContactType ContactType { get; set; } = null!;
    }
}
