using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class Article
    {
        public Article()
        {
            Comments = new HashSet<Comment>();
        }

        public int ArticleId { get; set; }
        public string Text { get; set; }
        public string ArticleName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
