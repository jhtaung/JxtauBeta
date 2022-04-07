using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppealClaimDetail
    {
        /// <summary>
        /// Unique ID for table
        /// </summary>
        public int AppealClaimDetailsId { get; set; }
        /// <summary>
        /// Claim ID associated with the Appeal
        /// </summary>
        public int AppealClaimId { get; set; }
        /// <summary>
        /// Claim Line associated with the Claim
        /// </summary>
        public int ClaimLine { get; set; }
        /// <summary>
        /// Service code of the service provided for the claim
        /// </summary>
        public string ServiceCode { get; set; } = null!;
        /// <summary>
        /// Date of service for the claim
        /// </summary>
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        /// <summary>
        /// Description of the service provided
        /// </summary>
        public string? ServiceDescription { get; set; }
        /// <summary>
        /// Status of the claim line
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// Amount of patient responsibility
        /// </summary>
        public decimal? PatientResp { get; set; }
        /// <summary>
        /// Amount of Co-Pay owed
        /// </summary>
        public decimal? CoPay { get; set; }
        public decimal? PlanAllowed { get; set; }
        public decimal? PlanBenefit { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Comment { get; set; }
        public decimal? ProvDiscount { get; set; }

        public virtual AppealClaim AppealClaim { get; set; } = null!;
    }
}
