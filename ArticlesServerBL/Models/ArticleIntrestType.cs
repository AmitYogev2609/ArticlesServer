using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class ArticleIntrestType
    {
        public int ArticleId { get; set; }
        public int IntrestId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Interest Intrest { get; set; }
    }
}
