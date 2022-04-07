using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class Appeal
    {
        public Appeal()
        {
            AppealClaims = new HashSet<AppealClaim>();
            AppealContacts = new HashSet<AppealContact>();
            AppealEvents = new HashSet<AppealEvent>();
            AppealMemos = new HashSet<AppealMemo>();
            AppealStatusLogs = new HashSet<AppealStatusLog>();
        }

        /// <summary>
        /// Unique identifier for the appeal
        /// </summary>
        public int AppealId { get; set; }
        /// <summary>
        /// Joins to PlanType.PlanTypeID to get PlanType, e.g. Pension Plan, IAP, Active Health Plan. Joins to PlanType
        /// </summary>
        public int? PlanTypeId { get; set; }
        /// <summary>
        /// e.g. Employer Contracts, MPI Pension &amp; Health Plans, Health Eligibility. Change name from DeptCode to DepartmentCode. Joins to DepartmentCode.DepartmentCode
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// Participant ID - imported from OPUS
        /// </summary>
        public string Mpid { get; set; } = null!;
        /// <summary>
        /// This is the subject of the appeal
        /// </summary>
        public string? Subject { get; set; }
        /// <summary>
        /// Holds additional information about the appeal
        /// </summary>
        public string? AppealInfo { get; set; }
        /// <summary>
        /// Contains reference material used to research the appeal
        /// </summary>
        public string? PlanReference { get; set; }
        /// <summary>
        /// High level descirption of what the appeal is concerning
        /// </summary>
        public string? ExecSummary { get; set; }
        /// <summary>
        /// Free form information to help the board with the appeal
        /// </summary>
        public string? AdditionalInfo { get; set; }
        /// <summary>
        /// Recommendations on how to respond to the appeal
        /// </summary>
        public string? Recommendations { get; set; }
        /// <summary>
        /// HOlds additional analysis about the appeal
        /// </summary>
        public string? Analysis { get; set; }
        /// <summary>
        /// Contains documents used to support the appeal and the pages that address the issue
        /// </summary>
        public string? SupportingDocs { get; set; }
        /// <summary>
        /// Locks/Unlocis the Appeal, no additinoal changes can be added when locked
        /// </summary>
        public bool? Lock { get; set; }
        /// <summary>
        /// Same as VIP
        /// </summary>
        public bool? Rap { get; set; }
        public bool? IsPrecedentEstablished { get; set; }
        /// <summary>
        /// Date Appeal was recieved.
        /// </summary>
        public DateTime? AppealReceivedDate { get; set; }
        /// <summary>
        /// Date Appeal will expire.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual PlanType? PlanType { get; set; }
        public virtual ICollection<AppealClaim> AppealClaims { get; set; }
        public virtual ICollection<AppealContact> AppealContacts { get; set; }
        public virtual ICollection<AppealEvent> AppealEvents { get; set; }
        public virtual ICollection<AppealMemo> AppealMemos { get; set; }
        public virtual ICollection<AppealStatusLog> AppealStatusLogs { get; set; }
    }
}
