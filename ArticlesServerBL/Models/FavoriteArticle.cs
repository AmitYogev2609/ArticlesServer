using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class FavoriteArticle
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }

        public virtual Article Article { get; set; }
        public virtual User User { get; set; }
    }
}
