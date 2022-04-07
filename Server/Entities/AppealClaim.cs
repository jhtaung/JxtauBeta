using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppealClaim
    {
        public AppealClaim()
        {
            AppealClaimDetails = new HashSet<AppealClaimDetail>();
        }

        /// <summary>
        /// Unique ID for table
        /// </summary>
        public int AppealClaimId { get; set; }
        /// <summary>
        /// Appeal ID Claim is linked to
        /// </summary>
        public int AppealId { get; set; }
        /// <summary>
        /// Claim ID associated with the appeal
        /// </summary>
        public string ClaimId { get; set; } = null!;
        public string? SecondaryId { get; set; }
        /// <summary>
        /// Status of the claim in QNXT
        /// </summary>
        public string? ClaimStatus { get; set; }
        /// <summary>
        /// Start date of the claim
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// How much the Provider is billing
        /// </summary>
        public decimal? Billed { get; set; }
        /// <summary>
        /// Amount of Co ordination of Benefits allowed
        /// </summary>
        public decimal? Coballowed { get; set; }
        /// <summary>
        /// Amount of Coordination of Beefits paid
        /// </summary>
        public decimal? Cobpaid { get; set; }
        public decimal? PlanPaid { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual Appeal Appeal { get; set; } = null!;
        public virtual ICollection<AppealClaimDetail> AppealClaimDetails { get; set; }
    }
}
