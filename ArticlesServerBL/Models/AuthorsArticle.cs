using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class AuthorsArticle
    {
        public int UsersId { get; set; }
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
        public virtual User Users { get; set; }
    }
}
