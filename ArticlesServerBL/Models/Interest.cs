using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class Interest
    {
        public Interest()
        {
            ArticleInterestTypes = new HashSet<ArticleInterestType>();
            FollwedInterests = new HashSet<FollwedInterest>();
        }

        public int InterestId { get; set; }
        public string InterestName { get; set; }
        public bool IsMajor { get; set; }

        public virtual ICollection<ArticleInterestType> ArticleInterestTypes { get; set; }
        public virtual ICollection<FollwedInterest> FollwedInterests { get; set; }
    }
}
