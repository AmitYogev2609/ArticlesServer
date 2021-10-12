using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class UserReport
    {
        public int ReportId { get; set; }
        public int UserIdReported { get; set; }

        public virtual Report Report { get; set; }
        public virtual User UserIdReportedNavigation { get; set; }
    }
}
