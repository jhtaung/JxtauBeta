using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class Department
    {
        public Department()
        {
            Appeals = new HashSet<Appeal>();
            Templates = new HashSet<Template>();
        }

        /// <summary>
        /// Unique Identifier for the Department
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// Unique Department Code, e.g. AUD, BRD, CLM
        /// </summary>
        public string DepartmentCode { get; set; } = null!;
        /// <summary>
        /// Unique Department Name
        /// </summary>
        public string DepartmentName { get; set; } = null!;
        /// <summary>
        /// Name of Department presenter
        /// </summary>
        public string? PresenterName { get; set; }
        /// <summary>
        /// Title of department presenter
        /// </summary>
        public string? PresenterTitle { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<Appeal> Appeals { get; set; }
        public virtual ICollection<Template> Templates { get; set; }
    }
}
