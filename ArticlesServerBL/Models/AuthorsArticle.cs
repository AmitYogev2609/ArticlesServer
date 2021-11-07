using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class AuthorsArticle
    {
        public int UserId { get; set; }
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
        public virtual User User { get; set; }
    }
}
