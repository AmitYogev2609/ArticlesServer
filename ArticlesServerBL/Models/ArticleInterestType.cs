using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class ArticleInterestType
    {
        public int ArticleId { get; set; }
        public int InterestId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
