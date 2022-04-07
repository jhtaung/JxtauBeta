using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class Template
    {
        /// <summary>
        /// Unique identifier for template
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>
        /// Dept ID the template is for
        /// </summary>
        public int? DepartmentId { get; set; }
        /// <summary>
        /// Subject the template addresses
        /// </summary>
        public string? SubjectTemplate { get; set; }
        /// <summary>
        /// General information about the appeal
        /// </summary>
        public string? AppealInfoTemplate { get; set; }
        /// <summary>
        /// References parts of the plan documents the appeal pertains to
        /// </summary>
        public string? PlanReferenceTemplate { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }
        public string? ExecSummaryTemplate { get; set; }
        public string? RecommendationsTemplate { get; set; }

        public virtual Department? Department { get; set; }
    }
}
