using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class PlanType
    {
        public PlanType()
        {
            Appeals = new HashSet<Appeal>();
        }

        /// <summary>
        /// Unique Identifier for the Plan
        /// </summary>
        public int PlanTypeId { get; set; }
        /// <summary>
        /// Plan type as it relates to Appeals, e.g. Active Health Plan, Retiree Health Plan, Pension and IAP Plans
        /// </summary>
        public string? PlanTypeName { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<Appeal> Appeals { get; set; }
    }
}
