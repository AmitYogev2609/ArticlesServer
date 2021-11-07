using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class ArticleReport
    {
        public int ArticleReportId { get; set; }
        public string Text { get; set; }
        public int UserIdReported { get; set; }
        public int ReportedArticleId { get; set; }

        public virtual Article ReportedArticle { get; set; }
        public virtual User UserIdReportedNavigation { get; set; }
    }
}
