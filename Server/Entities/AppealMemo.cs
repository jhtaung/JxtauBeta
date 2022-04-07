using System;
using System.Collections.Generic;

namespace Server.Entities
{
    public partial class AppealMemo
    {
        public int AppealMemoId { get; set; }
        public int AppealId { get; set; }
        public string? MemoFrom { get; set; }
        public DateTime MemoDate { get; set; }
        public string? MemoSubject { get; set; }
        public string? MemoText { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; } = null!;
        public DateTime UpdateDate { get; set; }

        public virtual Appeal Appeal { get; set; } = null!;
    }
}
