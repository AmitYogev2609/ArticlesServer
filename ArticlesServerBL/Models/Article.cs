using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleInterestTypes = new HashSet<ArticleInterestType>();
            ArticleReports = new HashSet<ArticleReport>();
            AuthorsArticles = new HashSet<AuthorsArticle>();
            Comments = new HashSet<Comment>();
        }

        public int ArticleId { get; set; }
        public string HtmlText { get; set; }
        public string ArticleName { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ArticleInterestType> ArticleInterestTypes { get; set; }
        public virtual ICollection<ArticleReport> ArticleReports { get; set; }
        public virtual ICollection<AuthorsArticle> AuthorsArticles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
