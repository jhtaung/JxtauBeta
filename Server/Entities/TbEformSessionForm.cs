namespace Server.Entities
{
    public partial class TbEformSessionForm
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int EFormSessionFormId { get; set; }
        /// <summary>
        /// eForm session id
        /// </summary>
        public int EFormSessionId { get; set; }
        /// <summary>
        /// eForm id
        /// </summary>
        public int EFormId { get; set; }
        /// <summary>
        /// Determines whether the doument is submitted
        /// </summary>
        public bool IsSubmitted { get; set; }
        /// <summary>
        /// Log the eform submit Informations
        /// </summary>
        public string? LogInfo { get; set; }
        /// <summary>
        /// Timestamp of when the record was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Timestamp of when the record was last modified
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }
    }
}
